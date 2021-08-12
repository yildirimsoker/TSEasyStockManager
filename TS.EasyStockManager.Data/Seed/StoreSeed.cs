using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TS.EasyStockManager.Data.Entity;

namespace TS.EasyStockManager.Data.Seed
{
    internal class StoreSeed : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.HasData(new Store { Id = 1, StoreCode = "EX01", StoreName = "Example Store", CreateDate = DateTime.Now });
        }
    }
}
