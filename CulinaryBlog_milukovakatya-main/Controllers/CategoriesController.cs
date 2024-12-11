using AutoMapper;
using CulinaryBlog.Domain.ViewModels;
using CulinaryBlog.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CulinaryBlog_milukovakatya.Controllers
{
    public class CategoriesController : Controller 
    {
        private readonly ICategoriesService _categoriesService;
        private IMapper _mapper { get; set; }

        MapperConfiguration mapperConfiguration = new MapperConfiguration(p =>
        {
            p.AddProfile<AppMappingProfile>();
        });
        public CategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
            _mapper = mapperConfiguration.CreateMapper();
        }
        public IActionResult ListOfCategories()
        {
            var result = _categoriesService.GetAllCategories();
            var listofCategories = _mapper.Map<List<CategoriesViewModel>>(result.Data);

            return View(listofCategories);
        }

    }
}
