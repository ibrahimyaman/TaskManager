using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TaskManager.Core.Entities;

namespace TaskManager.Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        protected static decimal Yuvarla(decimal deger)
        {
            return decimal.Round(deger, 2);
        }
        public void Add(TEntity entity)
        {
            using (var context = new TContext())
            {                
                var addedEntity =  context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }
        public async Task AddAsync(TEntity entity)
        {
            using (var context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                await context.SaveChangesAsync();
            }
        }
        public void AddRange(params TEntity[] entities)
        {
            using (var context = new TContext())
            {
                foreach (var entity in entities)
                {
                    var deletedEntity = context.Entry(entity);
                    deletedEntity.State = EntityState.Added;
                }
                context.SaveChanges();
            }
        }
        public async Task AddRangeAsync(params TEntity[] entities)
        {
            using (var context = new TContext())
            {
                foreach (var entity in entities)
                {
                    var deletedEntity = context.Entry(entity);
                    deletedEntity.State = EntityState.Added;
                }
                await context.SaveChangesAsync();
            }
        }


        public void Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
        public async Task DeleteAsync(TEntity entity)
        {
            using (var context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                await context.SaveChangesAsync();
            }
        }
        public void DeleteRange(params TEntity[] entities)
        {
            using (var context = new TContext())
            {
                foreach (var entity in entities)
                {
                    var deletedEntity = context.Entry(entity);
                    deletedEntity.State = EntityState.Deleted;
                }
                context.SaveChanges();
            }
        }
        public async Task DeleteRangeAsync(params TEntity[] entities)
        {
            using (var context = new TContext())
            {
                foreach (var entity in entities)
                {
                    var deletedEntity = context.Entry(entity);
                    deletedEntity.State = EntityState.Deleted;
                }
                await context.SaveChangesAsync();
            }
        }


        public void Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        public async Task UpdateAsync(TEntity entity)
        {
            using (var context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }
        public void UpdateRange(params TEntity[] entities)
        {
            using (var context = new TContext())
            {
                foreach (var entity in entities)
                {
                    var deletedEntity = context.Entry(entity);
                    deletedEntity.State = EntityState.Modified;
                }
                context.SaveChanges();
            }
        }
        public async Task UpdateRangeAsync(params TEntity[] entities)
        {
            using (var context = new TContext())
            {
                foreach (var entity in entities)
                {
                    var deletedEntity = context.Entry(entity);
                    deletedEntity.State = EntityState.Modified;
                }
                await context.SaveChangesAsync();
            }
        }


        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }
        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = new TContext())
            {
                return await context.Set<TEntity>().SingleOrDefaultAsync(filter);
            }
        }


        public IList<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                return filter == null ? context.Set<TEntity>().ToList() : 
                                        context.Set<TEntity>().Where(filter).ToList();
            }
        }
        public async Task<IList<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                return  filter == null ? await context.Set<TEntity>().ToListAsync() :
                                        await context.Set<TEntity>().Where(filter).ToListAsync();
            }
        }
    }
}
