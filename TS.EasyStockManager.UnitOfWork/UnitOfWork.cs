using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TS.EasyStockManager.Core.Repository;
using TS.EasyStockManager.Core.UnitOfWorks;
using TS.EasyStockManager.Data.Context;
using TS.EasyStockManager.Repository.Category;
using TS.EasyStockManager.Repository.Product;
using TS.EasyStockManager.Repository.Store;
using TS.EasyStockManager.Repository.StoreStock;
using TS.EasyStockManager.Repository.Transaction;
using TS.EasyStockManager.Repository.TransactionDetail;
using TS.EasyStockManager.Repository.TransactionType;
using TS.EasyStockManager.Repository.UnitOfMeasure;
using TS.EasyStockManager.Repository.User;

namespace TS.EasyStockManager.UnitOfWork
{
    public class UnitOfWork : IUnitOfWorks
    {
        private readonly EasyStockManagerDbContext _context;
        private IDbContextTransaction _transaction;
        private bool _disposed = false;

        public UnitOfWork(EasyStockManagerDbContext easyStockManagerDbContext)
        {
            _context = easyStockManagerDbContext;
        }
        private ICategoryRepository iCategoryRepository;
        private IProductRepository iProductRepository;
        private IStoreRepository iStoreRepository;
        private IStoreStockRepository iStoreStockRepository;
        private ITransactionDetailRepository iTransactionDetailRepository;
        private ITransactionRepository iTransactionRepository;
        private ITransactionTypeRepository iTransactionTypeRepository;
        private IUnitOfMeasureRepository iUnitOfMeasureRepository;
        private IUserRepository iUserRepository;

        public IProductRepository ProductRepository
        {
            get
            {
                if (iProductRepository == null)
                    iProductRepository = new ProductRepository(_context);
                return iProductRepository;
            }
        }

        public ICategoryRepository CategoryRepository
        {
            get
            {
                if (iCategoryRepository == null)
                    iCategoryRepository = new CategoryRepository(_context);
                return iCategoryRepository;
            }
        }

        public IStoreRepository StoreRepository
        {
            get
            {
                if (iStoreRepository == null)
                    iStoreRepository = new StoreRepository(_context);
                return iStoreRepository;
            }
        }

        public IStoreStockRepository StoreStockRepository
        {
            get
            {
                if (iStoreStockRepository == null)
                    iStoreStockRepository = new StoreStockRepository(_context);
                return iStoreStockRepository;
            }
        }

        public ITransactionDetailRepository TransactionDetailRepository
        {
            get
            {
                if (iTransactionDetailRepository == null)
                    iTransactionDetailRepository = new TransactionDetailRepository(_context);
                return iTransactionDetailRepository;
            }
        }
        public ITransactionRepository TransactionRepository
        {
            get
            {
                if (iTransactionRepository == null)
                    iTransactionRepository = new TransactionRepository(_context);
                return iTransactionRepository;
            }
        }
        public ITransactionTypeRepository TransactionTypeRepository
        {
            get
            {
                if (iTransactionTypeRepository == null)
                    iTransactionTypeRepository = new TransactionTypeRepository(_context);
                return iTransactionTypeRepository;
            }
        }

        public IUnitOfMeasureRepository UnitOfMeasureRepository
        {
            get
            {
                if (iUnitOfMeasureRepository == null)
                    iUnitOfMeasureRepository = new UnitOfMeasureRepository(_context);
                return iUnitOfMeasureRepository;
            }
        }
        public IUserRepository UserRepository
        {
            get
            {
                if (iUserRepository == null)
                    iUserRepository = new UserRepository(_context);
                return iUserRepository;
            }
        }

        public void Commit()
        {
            if (_transaction != null)
                _transaction.Commit();
        }

        public void CreateTransaction()
        {
            if (_context != null)
                _transaction = _context.Database.BeginTransaction();
        }

        public void RollBack()
        {
            if (_transaction != null)
                _transaction.Rollback();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
           await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                    }
                }
            }
            this._disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
