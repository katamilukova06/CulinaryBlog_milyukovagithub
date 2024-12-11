using AutoMapper;
using CulinaryBlog.Domain.ModelsDb;
using CulinaryBlog.Domain.Response;
using CulinaryBlog.Domain.Validators;
using CulinaryBlog.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CulinaryBlog.Domain.Enum;
using CulinaryBlog.Domain.Models;

namespace CulinaryBlog.Service.Interfaces
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IBaseStorage<CategoriesDb> _categoriesStorage;
        private IMapper _mapper { get; set; }
        private CategoriesValidator _validationRules { get; set; }

        MapperConfiguration mapperConfiguration = new MapperConfiguration(p =>
        {
            p.AddProfile<AppMappingProfile>();
        });

        public CategoriesService(IBaseStorage<CategoriesDb> categoriesStorage)
        {
            _categoriesStorage = categoriesStorage;
            _mapper = mapperConfiguration.CreateMapper();
            _validationRules = new CategoriesValidator();
        }
        public BaseResponse<List<Categories>> GetAllCategories()
        {
            try
            {
                var categoriesDb = _categoriesStorage.GetAll().OrderBy(p => p.Name).ToList();
                var result = _mapper.Map<List<Categories>>(categoriesDb);
                if (result.Count == 0)
                {
                    return new BaseResponse<List<Categories>>()
                    {
                        Description = "Найдено 0 элементов",
                        StatusCode = StatusCode.OK
                    };
                }

                return new BaseResponse<List<Categories>>()
                {
                    Data = result,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Categories>>()
                {
                    Description= ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }


}
