using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TS.EasyStockManager.Core.Repository;
using TS.EasyStockManager.Data.Context;
using TS.EasyStockManager.Repository.Base;

namespace TS.EasyStockManager.Repository.StoreStock
{
    public class StoreStockRepository : Repository<TS.EasyStockManager.Data.Entity.StoreStock>, IStoreStockRepository
    {
        private EasyStockManagerDbContext dbContext { get => _context as EasyStockManagerDbContext; }
        public StoreStockRepository(DbContext context) : base(context)
        {
        }

        public async Task RemoveByStoreAndProductId(int productId, int storeId)
        {
            var entity = await dbContext.StoreStock.FirstOrDefaultAsync(x => x.StoreId == storeId && x.ProductId == productId);
            if (entity != null)
                dbContext.StoreStock.Remove(entity);
        }

        public async Task<TS.EasyStockManager.Data.Entity.StoreStock> GetByStoreAndProductId(int productId, int storeId)
        {
            var entity = await dbContext.StoreStock.FirstOrDefaultAsync(x => x.StoreId == storeId && x.ProductId == productId);
            return entity;
        }

        
    }
}
