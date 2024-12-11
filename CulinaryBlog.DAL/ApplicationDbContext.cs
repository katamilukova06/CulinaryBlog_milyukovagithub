using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Design;
using System.ComponentModel.DataAnnotations;
using CulinaryBlog.Domain.ModelsDb;


namespace CulinaryBlog.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet <UserDb> UserDb { get; set; }
        public DbSet<RecipesDb> RecipesDb { get; set; }
        public DbSet<CategoriesDb> CategoriesDb { get; set; }
        public DbSet<CommentsRatingDb> CommentsRatingDb { get; set; }
        public DbSet<PictureRecipesDb> PictureRecipesDbs { get; set; }

        protected readonly IConfiguration Configuration;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
