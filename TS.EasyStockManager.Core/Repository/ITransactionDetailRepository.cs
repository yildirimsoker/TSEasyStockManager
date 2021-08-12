using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.EasyStockManager.Data.Entity;

namespace TS.EasyStockManager.Core.Repository
{
    public interface ITransactionDetailRepository : IRepository<TS.EasyStockManager.Data.Entity.TransactionDetail>
    {
        void DeleteAllRecordByTransaction(ICollection<TS.EasyStockManager.Data.Entity.TransactionDetail> transactionDetails);
        Task<IEnumerable<TS.EasyStockManager.Data.Entity.TransactionDetail>> GetByTransactionId(int transactionId);
    }
}
