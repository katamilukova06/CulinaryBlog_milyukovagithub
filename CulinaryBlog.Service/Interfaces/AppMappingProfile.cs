using CulinaryBlog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CulinaryBlog.Domain.ModelsDb;
using AutoMapper;
using CulinaryBlog.Domain.ViewModels.LoginAndRegistration;
using CulinaryBlog.Domain.ViewModels;

namespace CulinaryBlog.Service.Interfaces
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<User, UserDb>().ReverseMap();
            CreateMap<User, LoginViewModel>().ReverseMap();
            CreateMap<User, RegisterViewModel>().ReverseMap();
            CreateMap<RegisterViewModel, ConfirmEmailViewModel>().ReverseMap();
            CreateMap<User, ConfirmEmailViewModel>().ReverseMap();
            CreateMap<Categories, CategoriesDb>().ReverseMap();
            CreateMap<Categories, CategoriesViewModel>().ReverseMap();
            CreateMap<Recipes, RecipesDb>().ReverseMap();
            CreateMap<Recipes, RecipesForListOfRecipesViewModel>().ReverseMap();
            CreateMap<Recipes, RecipesPageViewModel>().ReverseMap();
            CreateMap<PictureRecipes, PictureRecipesDb>().ReverseMap();
            CreateMap<PictureRecipes, PictureRecipesViewModel>().ReverseMap();
        }
    }
}
