using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.EasyStockManager.Core.Repository;
using TS.EasyStockManager.Data.Context;
using TS.EasyStockManager.Repository.Base;

namespace TS.EasyStockManager.Repository.TransactionDetail
{
    public class TransactionDetailRepository : Repository<TS.EasyStockManager.Data.Entity.TransactionDetail>, ITransactionDetailRepository
    {
        private EasyStockManagerDbContext dbContext { get => _context as EasyStockManagerDbContext; }
        public TransactionDetailRepository(DbContext context) : base(context)
        {
        }

        public void DeleteAllRecordByTransaction(ICollection<TS.EasyStockManager.Data.Entity.TransactionDetail> transactionDetails)
        {
            dbContext.RemoveRange(transactionDetails);
        }

        public async Task<IEnumerable<TS.EasyStockManager.Data.Entity.TransactionDetail>> GetByTransactionId(int transactionId)
        {
            return await dbContext.TransactionDetail.Include(x => x.Product).ThenInclude(x=> x.UnitOfMeasure).Where(x => x.TransactionId == transactionId).ToListAsync();
        }
    }
}
