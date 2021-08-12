using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TS.EasyStockManager.Core.Repository
{
    public interface IProductRepository : IRepository<TS.EasyStockManager.Data.Entity.Product>
    {
        Task DeleteProductImage(int id);
        Task<IEnumerable<TS.EasyStockManager.Data.Entity.Product>> GetProductsByBarcodeAndName(string term);
    }
}
