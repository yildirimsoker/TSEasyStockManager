using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TS.EasyStockManager.Core.Repository;

namespace TS.EasyStockManager.Repository.Base
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter = null,  Expression<Func<TEntity, object>> orderByDesc = null, int? skip = null, int? take = null, IList<string> includes = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (includes != null && includes.Count() > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (filter != null)
                query = query.Where(filter);

            if (orderByDesc != null)
                query = query.OrderByDescending(orderByDesc);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return await query.AsNoTracking().ToListAsync();
        }


        public Task<int> FindCountAsync(Expression<Func<TEntity, bool>> filter = null, IList<string> includes = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (includes != null && includes.Count() > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (filter != null)
                query = query.Where(filter);

            return query.CountAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(filter);
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public async Task RemoveById(int id)
        {
            TEntity entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity != null)
                _context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }
    }
}
