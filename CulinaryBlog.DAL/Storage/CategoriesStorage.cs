﻿using CulinaryBlog.DAL.Interfaces;
using CulinaryBlog.Domain.ModelsDb;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryBlog.DAL.Storage
{
    public class CategoriesStorage : IBaseStorage<CategoriesDb>
    {
        public readonly ApplicationDbContext _db;

        public CategoriesStorage(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task Add(CategoriesDb item)
        {
            await _db.AddAsync(item);
            await _db.SaveChangesAsync();
        }
        public async Task Delete(CategoriesDb item)
        {
            _db.Remove(item);
            await _db.SaveChangesAsync();
        }
        public async Task<CategoriesDb> Get(Guid id)
        {
            return await _db.CategoriesDb.FirstOrDefaultAsync(x => x.Id == id);
        }
        public IQueryable<CategoriesDb> GetAll()
        {
            return _db.CategoriesDb;
        }
        public async Task<CategoriesDb> Update(CategoriesDb item)
        {
            _db.CategoriesDb.Update(item);
            await _db.SaveChangesAsync();
            return item;
        }
    }
}
