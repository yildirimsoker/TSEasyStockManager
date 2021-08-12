using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TS.EasyStockManager.Core.Repository
{
    public interface ITransactionRepository : IRepository<TS.EasyStockManager.Data.Entity.Transaction>
    {
        Task<TS.EasyStockManager.Data.Entity.Transaction> GetWithDetailById(int id);
        Task<TS.EasyStockManager.Data.Entity.Transaction> GetWithDetailAndProductById(int id);
    }
}
