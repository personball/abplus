using System.Data.Entity;
using Abp.Domain.Entities;
using Abp.Domain.Repositories.Ext;
using System.Data.Common;
using System.Data;
using System;
using System.Linq.Expressions;
using Dapper;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Abp.EntityFramework.Repositories.Ext
{
    /// <summary>
    /// 扩展 EfRepositoryBase 继承于  Abp.EntityFramework.Repositories.EfRepositoryBase
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public class EfRepositoryBase<TDbContext, TEntity> : EfRepositoryBase<TDbContext, TEntity, int>, IRepository<TEntity>
        where TEntity : class, IEntity<int>
        where TDbContext : DbContext
    {
        public EfRepositoryBase(IDbContextProvider<TDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
    /// <summary>
    /// 扩展 EfRepositoryBase 继承于  Abp.EntityFramework.Repositories.EfRepositoryBase
    /// </summary>
    /// <typeparam name="TDbContext">DbContext which contains <typeparamref name="TEntity"/>.</typeparam>
    /// <typeparam name="TEntity">Type of the Entity for this repository</typeparam>
    /// <typeparam name="TPrimaryKey">Primary key of the entity</typeparam>
    public class EfRepositoryBase<TDbContext, TEntity, TPrimaryKey> : Repositories.EfRepositoryBase<TDbContext, TEntity, TPrimaryKey>, IRepositoryWithDbContext
        where TEntity : class, IEntity<TPrimaryKey>
        where TDbContext : DbContext
    {
        public EfRepositoryBase(IDbContextProvider<TDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        #region Dapper
        public virtual DbConnection Connection
        {
            get
            {
                var context = Context;
                if (context != null)
                {
                    var database = context.Database;
                    if (database != null)
                    {
                        return database.Connection;
                    }
                }
                return null;
            }
        }
        /// <summary>
        ///     Gets the active transaction. If Dapper is active then <see cref="IUnitOfWork" /> should be started before
        ///     and there must be an active transaction. To Activate Dapper Use <see cref="DbContextEfTransactionStrategy" />
        /// </summary>
        /// <value>
        ///     The active transaction.
        /// </value>
        public virtual IDbTransaction ActiveTransaction
        {
            get
            {
                var context = Context;
                if (context != null)
                {
                    var database = context.Database;
                    if (database != null)
                    {
                        var CurrentTransaction = database.CurrentTransaction;
                        if (CurrentTransaction != null)
                        {
                            return CurrentTransaction.UnderlyingTransaction;
                        }
                    }
                }
                return null;
            }
        }

        public virtual IEnumerable<TEntity> Query(string query, object parameters = null)
        {
            return Connection.Query<TEntity>(query, parameters, ActiveTransaction);
        }
        public virtual Task<IEnumerable<TEntity>> QueryAsync(string query, object parameters = null)
        {
            return Connection.QueryAsync<TEntity>(query, parameters, ActiveTransaction);
        }



        public virtual IEnumerable<TAny> Query<TAny>(string query, object parameters = null)
        {
            return Connection.Query<TAny>(query, parameters, ActiveTransaction);
        }
        public virtual Task<IEnumerable<TAny>> QueryAsync<TAny>(string query, object parameters = null)
        {
            return Connection.QueryAsync<TAny>(query, parameters, ActiveTransaction);
        }

        public virtual IEnumerable<TAny> Query<TAny>(string query, List<object> parameters)
        {
            DynamicParameters dyParam = ToDynamicParameters(parameters);
            return Connection.Query<TAny>(query, dyParam, ActiveTransaction);
        }
        public virtual Task<IEnumerable<TAny>> QueryAsync<TAny>(string query, List<object> parameters)
        {
            DynamicParameters dyParam = ToDynamicParameters(parameters);
            return Connection.QueryAsync<TAny>(query, dyParam, ActiveTransaction);
        }

        private DynamicParameters ToDynamicParameters(List<object> parameters)
        {
            Dapper.DynamicParameters dyParam = new DynamicParameters();
            foreach (var item in parameters)
            {
                dyParam.AddDynamicParams(item);
            }

            return dyParam;
        }
        #endregion

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