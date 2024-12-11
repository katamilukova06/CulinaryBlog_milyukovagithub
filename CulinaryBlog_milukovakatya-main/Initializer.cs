using CulinaryBlog.Service.Interfaces;
using CulinaryBlog.Service;
using Microsoft.Extensions.DependencyInjection;
using CulinaryBlog.DAL.Interfaces;
using CulinaryBlog.Domain.ModelsDb;
using CulinaryBlog.DAL.Storage;

namespace CulinaryBlog_milukovakatya
{
    public static class Initializer
    { 
        public static void InitializeRepositories(this IServiceCollection services)
        {;
            services.AddScoped<IBaseStorage<UserDb>, UserStorage>();
            services.AddScoped<IBaseStorage<CategoriesDb>, CategoriesStorage>();
            services.AddScoped<IBaseStorage<RecipesDb>, RecipesStorage>();
            services.AddScoped<IBaseStorage<PictureRecipesDb>, PictureRecipesStorage>();
        }
        public static void InitializeServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICategoriesService, CategoriesService>();
            services.AddScoped<IRecipesService, RecipesService>();

            services.AddControllersWithViews()
            .AddDataAnnotationsLocalization()
            .AddViewLocalization();
        }
    }

}
