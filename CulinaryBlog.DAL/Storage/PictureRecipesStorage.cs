using CulinaryBlog.DAL.Interfaces;
using CulinaryBlog.Domain.ModelsDb;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryBlog.DAL.Storage
{
    public class PictureRecipesStorage : IBaseStorage<PictureRecipesDb>
    {
        public readonly ApplicationDbContext _db;

        public PictureRecipesStorage(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task Add(PictureRecipesDb item)
        {
            await _db.AddAsync(item);
            await _db.SaveChangesAsync();
        }
        public async Task Delete(PictureRecipesDb item)
        {
            _db.Remove(item);
            await _db.SaveChangesAsync();
        }
        public async Task<PictureRecipesDb> Get(Guid id)
        {
            return await _db.PictureRecipesDbs.FirstOrDefaultAsync(x => x.Id == id);
        }
        public IQueryable<PictureRecipesDb> GetAll()
        {
            return _db.PictureRecipesDbs;
        }
        public async Task<PictureRecipesDb> Update(PictureRecipesDb item)
        {
            _db.PictureRecipesDbs.Update(item);
            await _db.SaveChangesAsync();
            return item;
        }
    }
}
