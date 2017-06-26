using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Abp.Domain.Repositories.Ext
{
    /// <summary>
    /// 扩展 IRepository 继承于  Abp.Domain.Repositories.IRepository
    /// </summary>
    /// <typeparam name="TEntity">Main Entity type this repository works on</typeparam>
    public interface IRepository<TEntity> : IRepository<TEntity, int>
            where TEntity : class, IEntity<int>
    {

    }
    /// <summary>
    /// 扩展 IRepository 继承于  Abp.Domain.Repositories.IRepository
    /// </summary>
    /// <typeparam name="TEntity">Main Entity type this repository works on</typeparam>
    /// <typeparam name="TPrimaryKey">Primary key type of the entity</typeparam>
    public interface IRepository<TEntity, TPrimaryKey> : Repositories.IRepository<TEntity, TPrimaryKey>
            where TEntity : class, IEntity<TPrimaryKey>
    {
        #region Dapper
        /// <summary>
        ///     Queries the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        IEnumerable<TEntity> Query(string query, object parameters = null);
        /// <summary>
        ///     Queries the asynchronous.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> QueryAsync(string query, object parameters = null);


        /// <summary>
        ///     Queries the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        IEnumerable<TAny> Query<TAny>(string query, object parameters = null);
        /// <summary>
        ///     Queries the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>        
        Task<IEnumerable<TAny>> QueryAsync<TAny>(string query, object parameters = null);

        /// <summary>
        ///     Queries the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        IEnumerable<TAny> Query<TAny>(string query, List<object> parameters);
        /// <summary>
        ///     Queries the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        Task<IEnumerable<TAny>> QueryAsync<TAny>(string query, List<object> parameters);
        #endregion

        #region InsertOrUpdate
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
        #endregion
    }
}
