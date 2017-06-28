using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Domain.Uow;
using Abp.EntityFramework;
using Abp.EntityFramework.Uow;
using Dapper;
using Abp.Domain.Repositories;

namespace Abp.Dapper.Repositories
{
    public class DapperRepositoryBase<TDbContext, TEntity, TPrimaryKey> : IDapperRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
        where TDbContext : DbContext
    {
        private readonly IDbContextProvider<TDbContext> _dbContextProvider;

        public DapperRepositoryBase(IDbContextProvider<TDbContext> dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
        }

        public virtual TDbContext Context
        {
            get { return _dbContextProvider.GetDbContext(); }
        }

        public virtual DbConnection Connection
        {
            get { return Context.Database.Connection; }
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
            get { return Context.Database.CurrentTransaction.UnderlyingTransaction; }
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
            DynamicParameters dyParam = new DynamicParameters();
            foreach (var item in parameters)
            {
                dyParam.AddDynamicParams(item);
            }

            return dyParam;
        }
    }
}
