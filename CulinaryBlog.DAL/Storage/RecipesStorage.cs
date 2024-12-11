using CulinaryBlog.DAL.Interfaces;
using CulinaryBlog.Domain.ModelsDb;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryBlog.DAL.Storage
{
    public class RecipesStorage : IBaseStorage<RecipesDb>
    {
        public readonly ApplicationDbContext _db;

        public RecipesStorage(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task Add(RecipesDb item)
        {
            await _db.AddAsync(item);
            await _db.SaveChangesAsync();
        }
        public async Task Delete(RecipesDb item)
        {
            _db.Remove(item);
            await _db.SaveChangesAsync();
        }
        public async Task<RecipesDb> Get(Guid id)
        {
            return await _db.RecipesDb.FirstOrDefaultAsync(x => x.Id == id);
        }
        public IQueryable<RecipesDb> GetAll()
        {
            return _db.RecipesDb;
        }
        public async Task<RecipesDb> Update(RecipesDb item)
        {
            _db.RecipesDb.Update(item);
            await _db.SaveChangesAsync();
            return item;
        }
    }
}
