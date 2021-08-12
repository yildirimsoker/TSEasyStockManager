using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TS.EasyStockManager.Data.Entity;

namespace TS.EasyStockManager.Data.Seed
{
    internal class UnitOfMeasureSeed : IEntityTypeConfiguration<UnitOfMeasure>
    {
        public void Configure(EntityTypeBuilder<UnitOfMeasure> builder)
        {
            builder.HasData(new UnitOfMeasure { Id = 1, UnitOfMeasureName = "Piece", Isocode = "pc", CreateDate = DateTime.Now });
            builder.HasData(new UnitOfMeasure { Id = 2, UnitOfMeasureName = "Kilogram", Isocode = "kg", CreateDate = DateTime.Now });
            builder.HasData(new UnitOfMeasure { Id = 3, UnitOfMeasureName = "Meter", Isocode = "m", CreateDate = DateTime.Now });
        }
    }
}
