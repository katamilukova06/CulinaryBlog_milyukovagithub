using AutoMapper;
using CulinaryBlog.DAL.Interfaces;
using CulinaryBlog.Domain.Models;
using CulinaryBlog.Domain.ModelsDb;
using CulinaryBlog.Domain.Response;
using CulinaryBlog.Domain.Enum;
using CulinaryBlog.Domain.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CulinaryBlog.Service.Interfaces
{
    public class RecipesService : IRecipesService
    {
        private readonly IBaseStorage<RecipesDb> _recipesStorage;
        private readonly IBaseStorage<PictureRecipesDb> _pictureStorage;

        private IMapper _mapper { get; set; }

        MapperConfiguration mapperConfiguration = new MapperConfiguration(p =>
        {
            p.AddProfile<AppMappingProfile>();
        });
        public RecipesService(IBaseStorage<RecipesDb> recipesStorage, IBaseStorage<PictureRecipesDb> pictureStorage)
        {
            _recipesStorage = recipesStorage;
            _mapper = mapperConfiguration.CreateMapper();
            _pictureStorage = pictureStorage;
        }
        public BaseResponse<List<Recipes>> GetAllRecipesByIdCategories(Guid Id)
        {
            try
            {
                var recipesDb = _recipesStorage.GetAll().Where(x => Id == x.CategoryId).OrderBy(p => p.Title).ToList();
                var result = _mapper.Map<List<Recipes>>(recipesDb);
                if (result.Count == 0)
                {
                    return new BaseResponse<List<Recipes>>()
                    {
                        Description = "Найдено 0 элементов",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<List<Recipes>>()
                {
                    Data = result,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Recipes>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public BaseResponse<List<Recipes>> GetRecipesByFilter(RecipesFilter filter)
        {
            try
            {
                var recipesFilter = GetAllRecipesByIdCategories(filter.IdCategory).Data;

                if (filter != null && recipesFilter != null)
                {
                    if (filter.TimeMax != 1000 || filter.TimeMin != 0)
                    {
                        recipesFilter = recipesFilter.Where(f => f.Time < filter.TimeMax && f.Time > filter.TimeMin).ToList();
                    }
                }
                return new BaseResponse<List<Recipes>>
                {
                    Data = recipesFilter,
                    Description = "Отфильтрованные данные",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Recipes>>
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
            
        }
        public async Task<BaseResponse<Recipes>> GetRecipesById(Guid id)
        {
            try
            {
                var recipesDb = await _recipesStorage.Get(id);

                var result = _mapper.Map<Recipes>(recipesDb);
                if(result == null)
                {
                    return new BaseResponse<Recipes>()
                    {
                        Description = "Найдено 0 элементов",
                        StatusCode = StatusCode.OK
                    };
                }

                return new BaseResponse<Recipes>()
                {
                    Data = result,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Recipes>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public BaseResponse<List<PictureRecipes>> GetPictureByIdRecipes(Guid Id)
        {
            try
            {
                var pictureRecipesDb = _pictureStorage.GetAll().Where(x => Id == x.IdRecipes).ToList();

                var result = _mapper.Map<List<PictureRecipes>>(pictureRecipesDb);
                if (result.Count == 0)
                {
                    return new BaseResponse<List<PictureRecipes>>()
                    {
                        Description = "Найдено 0 элементов",
                        StatusCode = StatusCode.OK
                    };
                }

                return new BaseResponse<List<PictureRecipes>>()
                {
                    Data = result,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<PictureRecipes>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

    }
}
