using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CulinaryBlog.Domain.Response;
using CulinaryBlog.Domain.Models;
using CulinaryBlog.Service.Interfaces;
using AutoMapper;
using CulinaryBlog.Domain.ModelsDb;
using CulinaryBlog.DAL.Interfaces;
using CulinaryBlog.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using CulinaryBlog.Domain.Helpers;
using CulinaryBlog.Domain.Validators;
using FluentValidation;
using MimeKit;
using System.IO;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;

namespace CulinaryBlog.Service
{
    public class AccountService : IAccountService
    {

        private readonly IBaseStorage<UserDb> _userStorage;
        private IMapper _mapper { get; set; }
        private UserValidator _validationRules { get; set; }

        MapperConfiguration mapperConfiguration = new MapperConfiguration(p =>
        {
            p.AddProfile<AppMappingProfile>();
        });
        public AccountService(IBaseStorage<UserDb> userStorage)
        {
            _userStorage = userStorage;
            _mapper = mapperConfiguration.CreateMapper();
            _validationRules= new UserValidator();
        }
        public async Task<BaseResponse<ClaimsIdentity>> Login(User model)
        {
            try
            {
                await _validationRules.ValidateAndThrowAsync(model);

                var userdb = await _userStorage.GetAll().FirstOrDefaultAsync(x =>x.Email == model.Email);

                if (userdb == null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Пользователь не найден"
                    };
                }
                if (userdb.Password != HashPasswordHelper.HashPassword(model.Password))
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Неверный пароль или почта"
                    };
                }
                model.Login = userdb.Login;
                model.Role = userdb.Role;
                var result = AuthenticateUserHelper.Authenticate(model);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    StatusCode = StatusCode.OK
                };
            }
            catch (ValidationException ex)
            {
                var errorMessages = string.Join(";", ex.Errors.Select(e => e.ErrorMessage));
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = errorMessages,
                    StatusCode= StatusCode.BadRequest
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<string>> Register(User model)
        {
            try
            {
                Random random = new Random();
                string confirmationCode = $"{random.Next(10)}{random.Next(10)}{random.Next(10)}";

                if (await _userStorage.GetAll().FirstOrDefaultAsync(x => x.Email == model.Email) != null)
                {
                    return new BaseResponse<string>()
                    {
                        Description = "Пользователь с такой почтой уже есть",
                    };
                }
                await SendEmail(model.Email, confirmationCode);
                return new BaseResponse<string>()
                {
                    Data = confirmationCode,
                    Description = "Письмо отправлено",
                    StatusCode = StatusCode.OK
                };
            }
            catch (ValidationException ex)
            {
                var errorMessages = string.Join(";", ex.Errors.Select(e => e.ErrorMessage));
                return new BaseResponse<string>()
                {
                    Description = errorMessages,
                    StatusCode = StatusCode.BadRequest
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<string>()
                {
                    Description= ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task SendEmail(string email, string confirmationCode)
        {
            string path = "D:\\College 3\\Educational Practice\\passwordPractice.txt";
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта", "CulinaryBlog_milukovakatya.ru"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = "Добро пожаловать!";
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = "<html>" + "<head>" + "<style>" +
                "body { font-family: Arial, sans-serif; background-color: #f2f2f2; }" +
                ".container { max-width: 600px; margin: 0 auto; padding: 20px; background-color: #fff; border-radius: 10px; box-shadow: 0px 0px 10px rgba(0,0,0,0.1); }" +
                ".header { text-align: center; margin-bottom: 20px; }" +
                ".message { font-size: 16px; line-height: 1.6; }" +
                ".conteiner-code { background-color: #f0f0f0; padding: 5px; border-radius: 5px; font-weight: bold; }" +
                ".code {text-align: center; }" +
                "</style>" +
                "</head>" +
                "<body> " +
                "<div class='container'>" +
                "<div class='header'><h1>Добро пожаловать на сайт Кулинарный блог!</h1></div>" +
                "<div class='message'>" +
                 "<р> Пожалуйста, введите данный код на сайте, чтобы подтвердить ваш еmail и завершить регистрацию:</p> " +
                "<div class='conteiner-code'><p class='code'>" + confirmationCode + "</p></div>" +
                "</div>" + "</div>" + "</body>" + "</html>"
            };
            using (StreamReader reader = new StreamReader(path))
            {
                string password = await reader.ReadToEndAsync();

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.gmail.com", 465, true);
                    await client.AuthenticateAsync("milukovaekaterina72@gmail.com", password);
                    await client.SendAsync(emailMessage);

                    await client.DisconnectAsync(true);
                }
            }
        }


        public async Task<BaseResponse<ClaimsIdentity>> ConfirmEmail(User model, string code, string confirmCode)
        {
            try
            {
                if (code != confirmCode)
                {
                    throw new Exception("Неверный код! Регистрация не выполнена.");
                }

                model.Password = HashPasswordHelper.HashPassword(model.Password);

                await _validationRules.ValidateAndThrowAsync(model);

                var userdb = _mapper.Map<UserDb>(model);

                await _userStorage.Add(userdb);

                var result = AuthenticateUserHelper.Authenticate(model);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    Description = "Объект добавился",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public async Task<BaseResponse<ClaimsIdentity>> IsCreatedAccount(User model)
        {
            try
            {
                var userDb = new UserDb();
                if(await _userStorage.GetAll().FirstOrDefaultAsync(x => x.Email == model.Email) == null)
                {
                    model.Password = "google";
                    
                    userDb = _mapper.Map<UserDb>(model);

                    await _userStorage.Add(userDb);

                    var resultRegister = AuthenticateUserHelper.Authenticate(model);
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Data = resultRegister,
                        Description = "Объект добавился",
                        StatusCode = StatusCode.OK
                    };
                }

                var resultLogin = AuthenticateUserHelper.Authenticate(model);
                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = resultLogin,
                    Description = "Объект уже был создан",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }

}
