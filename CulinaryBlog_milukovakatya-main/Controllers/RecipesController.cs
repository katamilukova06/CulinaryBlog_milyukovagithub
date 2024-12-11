using AutoMapper;
using CulinaryBlog.DAL.Interfaces;
using CulinaryBlog.Domain.Filter;
using CulinaryBlog.Domain.ModelsDb;
using CulinaryBlog.Domain.ViewModels;
using CulinaryBlog.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CulinaryBlog_milukovakatya.Controllers
{
    public class RecipesController : Controller
    {
        private readonly IRecipesService _recipesService;
        private IMapper _mapper { get; set; }
        MapperConfiguration mapperConfiguration = new MapperConfiguration(p =>
        {
            p.AddProfile<AppMappingProfile>();
        });
        public RecipesController(IRecipesService recipesService)
        {
            _recipesService = recipesService;
            _mapper = mapperConfiguration.CreateMapper();
        }
        public IActionResult ListOfRecipes(Guid Id)
        {
            var result = _recipesService.GetAllRecipesByIdCategories(Id);
            ListOfRecipesViewModel listRecipes = new ListOfRecipesViewModel
            {
                Recipes = _mapper.Map<List<RecipesForListOfRecipesViewModel>>(result.Data),
                CategoryId= Id
            };
            return View(listRecipes);
        }
        [HttpPost]
        public async Task<IActionResult> Filter([FromBody] RecipesFilter filter)
        {
            var result = _recipesService.GetRecipesByFilter(filter);

            var filteredRecipes = _mapper.Map<List<RecipesForListOfRecipesViewModel>>(result.Data);

            return Json(filteredRecipes);
        }
        public async Task<IActionResult> ListOfRecipesPage(Guid Id)
        {
            var resultRecipes = await _recipesService.GetRecipesById(Id);
            var resultPicture = _recipesService.GetPictureByIdRecipes(Id);

            RecipesPageViewModel recipes = _mapper.Map<RecipesPageViewModel>(resultRecipes.Data);
            recipes.PictureRecipes = _mapper.Map<List<PictureRecipesViewModel>>(resultPicture.Data);

            return View(recipes);
        }
    }
}
