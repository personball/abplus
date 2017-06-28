using Abp.Domain.Entities;

namespace Abp.Domain.Repositories
{
    public interface IDapperRepository<TEntity> : IDapperRepository<TEntity, int> where TEntity : class, IEntity<int>
    {
    }
}
