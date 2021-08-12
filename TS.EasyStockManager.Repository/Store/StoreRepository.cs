using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TS.EasyStockManager.Core.Repository;
using TS.EasyStockManager.Data.Context;
using TS.EasyStockManager.Repository.Base;

namespace TS.EasyStockManager.Repository.Store
{
    public class StoreRepository : Repository<TS.EasyStockManager.Data.Entity.Store>, IStoreRepository
    {
        private EasyStockManagerDbContext dbContext { get => _context as EasyStockManagerDbContext; }

        public StoreRepository(DbContext context) : base(context)
        {
        }
    }
}
