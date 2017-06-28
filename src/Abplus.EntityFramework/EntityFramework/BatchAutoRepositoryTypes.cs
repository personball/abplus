using Abp.Dapper.Repositories;
using Abp.Domain.Repositories;
using JetBrains.Annotations;

namespace Abp.EntityFramework
{
    public static class BatchAutoRepositoryTypes
    {
        static BatchAutoRepositoryTypes()
        {
            Default = new BatchAutoRepositoryTypesAttribute(
                typeof(IDapperRepository<>),
                typeof(IDapperRepository<,>),
                typeof(DapperRepositoryBase<,>),
                typeof(DapperRepositoryBase<,,>)
            );
        }

        public static BatchAutoRepositoryTypesAttribute Default { get; private set; }
    }
}
