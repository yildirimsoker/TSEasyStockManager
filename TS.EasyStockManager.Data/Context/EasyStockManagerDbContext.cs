using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TS.EasyStockManager.Data.Configurations;
using TS.EasyStockManager.Data.Entity;
using TS.EasyStockManager.Data.Seed;

namespace TS.EasyStockManager.Data.Context
{
    public class EasyStockManagerDbContext : DbContext
    {
        public EasyStockManagerDbContext(DbContextOptions<EasyStockManagerDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Store> Store { get; set; }
        public DbSet<StoreStock> StoreStock { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<TransactionDetail> TransactionDetail { get; set; }
        public DbSet<TransactionType> TransactionType { get; set; }
        public DbSet<UnitOfMeasure> UnitOfMeasure { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new StoreConfiguration());
            modelBuilder.ApplyConfiguration(new StoreStockConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionDetailConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UnitOfMeasureConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionTypeSeed());
            modelBuilder.ApplyConfiguration(new UnitOfMeasureSeed());
            modelBuilder.ApplyConfiguration(new UserSeed());
            modelBuilder.ApplyConfiguration(new StoreSeed());
            modelBuilder.ApplyConfiguration(new ProductSeed());
        }

    }
}
