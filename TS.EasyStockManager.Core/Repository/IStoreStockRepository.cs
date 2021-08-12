using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TS.EasyStockManager.Core.Repository
{
    public interface IStoreStockRepository : IRepository<TS.EasyStockManager.Data.Entity.StoreStock>
    {
        Task RemoveByStoreAndProductId(int productId, int storeId);
        Task<TS.EasyStockManager.Data.Entity.StoreStock> GetByStoreAndProductId(int productId, int storeId);
    }
}
