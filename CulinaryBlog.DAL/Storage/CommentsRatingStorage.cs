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
    public class CommentsRatingStorage : IBaseStorage<CommentsRatingDb>
    {
        public readonly ApplicationDbContext _db;

        public CommentsRatingStorage(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task Add(CommentsRatingDb item)
        {
            await _db.AddAsync(item);
            await _db.SaveChangesAsync();
        }
        public async Task Delete(CommentsRatingDb item)
        {
            _db.Remove(item);
            await _db.SaveChangesAsync();
        }
        public async Task<CommentsRatingDb> Get(Guid id)
        {
            return await _db.CommentsRatingDb.FirstOrDefaultAsync(x => x.Id == id);
        }
        public IQueryable<CommentsRatingDb> GetAll()
        {
            return _db.CommentsRatingDb;
        }
        public async Task<CommentsRatingDb> Update(CommentsRatingDb item)
        {
            _db.CommentsRatingDb.Update(item);
            await _db.SaveChangesAsync();
            return item;
        }
    }
}
