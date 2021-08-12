using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TS.EasyStockManager.Core.Repository;
using TS.EasyStockManager.Data.Context;
using TS.EasyStockManager.Repository.Base;

namespace TS.EasyStockManager.Repository.TransactionType
{
    public class TransactionTypeRepository : Repository<TS.EasyStockManager.Data.Entity.TransactionType>, ITransactionTypeRepository
    {
        private EasyStockManagerDbContext dbContext { get => _context as EasyStockManagerDbContext; }
        public TransactionTypeRepository(DbContext context) : base(context)
        {
        }
    }
}
