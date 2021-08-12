using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TS.EasyStockManager.Core.Repository;
using TS.EasyStockManager.Data.Context;
using TS.EasyStockManager.Repository.Base;

namespace TS.EasyStockManager.Repository.Transaction
{
    public class TransactionRepository : Repository<TS.EasyStockManager.Data.Entity.Transaction>, ITransactionRepository
    {
        private EasyStockManagerDbContext dbContext { get => _context as EasyStockManagerDbContext; }
        public TransactionRepository(DbContext context) : base(context)
        {
        }
        public async Task<TS.EasyStockManager.Data.Entity.Transaction> GetWithDetailById(int id)
        {
            return await dbContext.Transaction.Include(x => x.TransactionDetail).FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<TS.EasyStockManager.Data.Entity.Transaction> GetWithDetailAndProductById(int id)
        {
            return await dbContext.Transaction.Include(x => x.TransactionDetail).ThenInclude(x=> x.Product).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
