using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TaskManager.Core.Entities;

namespace TaskManager.Core.DataAccess
{
    public interface IEntityRepository<TEntity> where TEntity : class, IEntity, new()
    {
        TEntity Get(Expression<Func<TEntity, bool>> filter);
        IList<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter);
        Task<IList<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null);

        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);

        void AddRange(params TEntity[] entity);
        void UpdateRange(params TEntity[] entity);
        void DeleteRange(params TEntity[] entity);

        Task AddRangeAsync(params TEntity[] entity);
        Task UpdateRangeAsync(params TEntity[] entity);
        Task DeleteRangeAsync(params TEntity[] entity);
    }
}
