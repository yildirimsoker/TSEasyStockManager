using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TS.EasyStockManager.Core.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter = null,
                                             Expression<Func<TEntity, object>> orderByDesc = null,
                                             int? skip = null, int? take = null,
                                             IList<string> includes = null);
        Task<int> FindCountAsync(Expression<Func<TEntity, bool>> filter = null, IList<string> includes = null);
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter);
        void Remove(TEntity entity);
        Task RemoveById(int id);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
