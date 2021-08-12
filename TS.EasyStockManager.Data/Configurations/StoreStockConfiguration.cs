using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TS.EasyStockManager.Data.Entity;

namespace TS.EasyStockManager.Data.Configurations
{
    internal class StoreStockConfiguration : IEntityTypeConfiguration<StoreStock>
    {
        public void Configure(EntityTypeBuilder<StoreStock> builder)
        {
            builder.HasKey(x => new { x.StoreId, x.ProductId });
            builder.Property(x => x.Stock).HasColumnType("decimal(18,2)");
            builder.ToTable("StoreStock");
        }
    }
}
