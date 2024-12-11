
using CulinaryBlog_milukovakatya.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CulinaryBlog.Service.Interfaces;
using AutoMapper;
using CulinaryBlog.Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using CulinaryBlog.Domain.ViewModels.LoginAndRegistration;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Hosting;
using CulinaryBlog.Domain.Filter;
using CulinaryBlog.Domain.ViewModels;

namespace CulinaryBlog_milukovakatya.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IAccountService _accountService;

        private readonly IWebHostEnvironment _appEnvironment;

        private IMapper _mapper { get; set; }

        MapperConfiguration mapperConfiguration = new MapperConfiguration(p =>
        {
            p.AddProfile<AppMappingProfile>();
        });

        public HomeController(ILogger<HomeController> logger, IAccountService accountService, IWebHostEnvironment appEnvironment)
        {
            _accountService = accountService;
            _logger = logger;
            _mapper = mapperConfiguration.CreateMapper();
            _appEnvironment = appEnvironment;
        }


        public IActionResult SiteInformation()
        {
            return View();
        }
        

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<User>(model);
                var response = await _accountService.Login(user);
                if(response.StatusCode == CulinaryBlog.Domain.Enum.StatusCode.OK)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(response.Data));
                    return Ok(model);
                }
                ModelState.AddModelError("", response.Description); 
            }  
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                                           .Select(e => e.ErrorMessage)
                                           .ToList();
            return BadRequest(errors);
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model) 
        { 
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<User>(model);

                var confirm = _mapper.Map<ConfirmEmailViewModel>(model);

                var code = await _accountService.Register(user);

                confirm.GeneratedCode = code.Data;

                return Ok(confirm);
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                                           .Select(e => e.ErrorMessage)
                                           .ToList();
            return BadRequest(errors);
        }
        [HttpPost]

        public async Task <IActionResult> ConfirmEmail([FromBody] ConfirmEmailViewModel confirmEmailModel)
        {
            var user = _mapper.Map<User>(confirmEmailModel);

            var response = await _accountService.ConfirmEmail(user, confirmEmailModel.GeneratedCode, confirmEmailModel.CodeConfirm);
        
            if (response.StatusCode == CulinaryBlog.Domain.Enum.StatusCode.OK)
            {
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(response.Data));
                return Ok(confirmEmailModel);
            }
            ModelState.AddModelError("", response.Description);

            var errors = ModelState.Values.SelectMany(v => v.Errors)
                                           .Select(e => e.ErrorMessage)
                                           .ToList();
            return BadRequest(errors);
        }


        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("SiteInformation", "Home");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task AuthenticationGoogle(string returnUrl = "/")
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
                new AuthenticationProperties
                {
                    RedirectUri = Url.Action("GoogleResponse", new { returnUrl } ),
                    Parameters = { { "prompt", "select_account"} }
                });
        }
        public async Task<IActionResult> GoogleResponse(string returnUrl = "/")
        {
            try
            {
                var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                if (result?.Succeeded == true)
                {
                    User model = new User
                    {
                        Login = result.Principal.FindFirst(ClaimTypes.Name)?.Value,
                        Email = result.Principal.FindFirst(ClaimTypes.Email)?.Value,

                    };
                    var response = await _accountService.IsCreatedAccount(model);
                    if (response.StatusCode == CulinaryBlog.Domain.Enum.StatusCode.OK)
                    {
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(response.Data));
                        return Redirect(returnUrl);
                    }
                }
                return BadRequest("Аутентификация не удалась.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
       
    }
}
