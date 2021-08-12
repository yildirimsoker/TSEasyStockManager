using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TS.EasyStockManager.Core.Repository;
using TS.EasyStockManager.Data.Context;
using TS.EasyStockManager.Repository.Base;

namespace TS.EasyStockManager.Repository.Category
{
    public class CategoryRepository : Repository<TS.EasyStockManager.Data.Entity.Category>, ICategoryRepository
    {
        private EasyStockManagerDbContext dbContext { get => _context as EasyStockManagerDbContext; }

        public CategoryRepository(DbContext context) : base(context)
        {
        }
    }
}
