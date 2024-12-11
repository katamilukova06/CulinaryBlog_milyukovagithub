
using CulinaryBlog.Domain.Models;
using CulinaryBlog.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CulinaryBlog.Service;
using System.Security.Claims;

namespace CulinaryBlog.Service.Interfaces
{
    public interface IAccountService
    {
        Task<BaseResponse<ClaimsIdentity>> Login(User model);
        Task<BaseResponse<string>> Register(User model);
        Task<BaseResponse<ClaimsIdentity>> ConfirmEmail(User model, string code, string confirmCode);
        Task<BaseResponse<ClaimsIdentity>> IsCreatedAccount(User model);
    }

    
}


