using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Abp.Timing;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Abp.EntityFramework.Repositories
{
    public class BatchEfRepositoryBase<TDbContext, TEntity, TPrimaryKey> :
        EfRepositoryBase<TDbContext, TEntity, TPrimaryKey>,
        IBatchRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
        where TDbContext : DbContext
    {

        public BatchEfRepositoryBase(
            IDbContextProvider<TDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public IEnumerable<TEntity> BatchInsert(IEnumerable<TEntity> entities)
        {
            return Table.AddRange(entities);
        }

        public async Task<IEnumerable<TEntity>> BatchInsertAsync(IEnumerable<TEntity> entities)
        {
            return await Task.FromResult(Table.AddRange(entities));
        }

        #region InsertOrUpdate
        public TEntity InsertOrUpdate(Expression<Func<TEntity, bool>> predicate, Action<TEntity> updateAction, Func<TEntity> insertFunc)
        {
            var entity = FirstOrDefault(predicate);
            bool needInsert = entity == null;
            if (needInsert)
            {
                entity = insertFunc();
            }
            updateAction(entity);
            if (needInsert)
            {
                Insert(entity);
            }
            else
            {
                Update(entity);
            }
            return entity;
        }
        public TPrimaryKey InsertOrUpdateAndGetId(Expression<Func<TEntity, bool>> predicate, Action<TEntity> updateAction, Func<TEntity> insertFunc)
        {
            var entity = InsertOrUpdate(predicate, updateAction, insertFunc);
            if (entity.IsTransient())
            {
                Context.SaveChanges();
            }
            return entity.Id;
        }
        #endregion
    }
}
