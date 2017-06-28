using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Abp.Domain.Entities;

namespace Abp.Domain.Repositories
{
    public interface IBatchRepository<TEntity, TPrimaryKey> :
        IRepository<TEntity, TPrimaryKey> where TEntity :
        class, IEntity<TPrimaryKey>
    {
        IEnumerable<TEntity> BatchInsert(IEnumerable<TEntity> entities);

        /// <summary>
        /// Batch insert, Notice : You can use Effort.EF6 to test
        /// </summary>
        /// <param name="entities">entities</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> BatchInsertAsync(IEnumerable<TEntity> entities);
      

        /// <summary>
        /// 新增或者修改
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="updateAction"></param>
        /// <param name="insertFunc"></param>
        /// <returns></returns>
        TEntity InsertOrUpdate(Expression<Func<TEntity, bool>> predicate, Action<TEntity> updateAction, Func<TEntity> insertFunc);
        /// <summary>
        /// 新增或者修改 获取Id
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="updateAction"></param>
        /// <param name="insertFunc"></param>
        /// <returns></returns>
        TPrimaryKey InsertOrUpdateAndGetId(Expression<Func<TEntity, bool>> predicate, Action<TEntity> updateAction, Func<TEntity> insertFunc);
    }
}
