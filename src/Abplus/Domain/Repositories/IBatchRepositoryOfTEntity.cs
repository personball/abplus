using Abp.Domain.Entities;

namespace Abp.Domain.Repositories
{
    public interface IBatchRepository<TEntity> : 
        IBatchRepository<TEntity, int> where TEntity : 
        class, IEntity<int>
    {

    }
}
