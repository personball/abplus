using System.Data.Entity;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;

namespace Abp.EntityFramework.Repositories
{
    public class BatchEfRepositoryBase<TDbContext, TEntity> : 
        BatchEfRepositoryBase<TDbContext, TEntity, int>, 
        IBatchRepository<TEntity>
        where TEntity : class, IEntity<int>
        where TDbContext : DbContext
    {
        public BatchEfRepositoryBase(
            IDbContextProvider<TDbContext> dbContextProvider
            ) : base(dbContextProvider)
        {
        }
    }
}
