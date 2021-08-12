using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TS.EasyStockManager.Core.Repository;
using TS.EasyStockManager.Data.Context;
using TS.EasyStockManager.Repository.Base;

namespace TS.EasyStockManager.Repository.UnitOfMeasure
{
    public class UnitOfMeasureRepository : Repository<TS.EasyStockManager.Data.Entity.UnitOfMeasure>, IUnitOfMeasureRepository
    {
        private EasyStockManagerDbContext dbContext { get => _context as EasyStockManagerDbContext; }
        public UnitOfMeasureRepository(DbContext context) : base(context)
        {
        }
    }
}
