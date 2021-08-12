using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TS.EasyStockManager.Data.Entity;

namespace TS.EasyStockManager.Data.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(30);
            builder.Property(x => x.Surname).IsRequired().HasMaxLength(30);
            builder.Property(x => x.Password).IsRequired().HasMaxLength(40);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(50);
            builder.HasIndex(u => u.Email).IsUnique();
            builder.ToTable("User");
        }
    }
}
