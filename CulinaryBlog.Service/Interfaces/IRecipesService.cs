using CulinaryBlog.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CulinaryBlog.Domain.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using CulinaryBlog.Domain.Filter;

namespace CulinaryBlog.Service.Interfaces
{
    public interface IRecipesService
    {
        BaseResponse<List<Recipes>> GetAllRecipesByIdCategories(Guid Id);
        BaseResponse<List<Recipes>> GetRecipesByFilter(RecipesFilter filter);
        Task<BaseResponse<Recipes>> GetRecipesById(Guid Id);
        BaseResponse<List<PictureRecipes>> GetPictureByIdRecipes(Guid Id);
    }
}
