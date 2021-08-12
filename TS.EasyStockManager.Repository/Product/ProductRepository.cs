using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.EasyStockManager.Core.Repository;
using TS.EasyStockManager.Data.Context;
using TS.EasyStockManager.Repository.Base;

namespace TS.EasyStockManager.Repository.Product
{
    public class ProductRepository : Repository<TS.EasyStockManager.Data.Entity.Product>, IProductRepository
    {
        private EasyStockManagerDbContext dbContext { get => _context as EasyStockManagerDbContext; }

        public ProductRepository(DbContext context) : base(context)
        {
        }

        public async Task DeleteProductImage(int id)
        {
            TS.EasyStockManager.Data.Entity.Product product = await dbContext.Product.FirstOrDefaultAsync(x => x.Id == id);

            if (product != null)
            {
                product.Image = null;
                dbContext.Entry(product).Property(f => f.Image).IsModified = true;
            }
        }

        public async Task<IEnumerable<Data.Entity.Product>> GetProductsByBarcodeAndName(string term)
        {
            return await dbContext.Product.Where(x => x.ProductName.Contains(term) || x.Barcode.Contains(term)).AsNoTracking().ToListAsync();
        }
    }
}
