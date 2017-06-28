using System;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.Reflection.Extensions;
using Abp.EntityFramework.Extensions;

namespace Abp.Dapper.Repositories
{
    public class DapperGenericRepositoryRegistrar : ITransientDependency
    {
        public static void RegisterForDbContext(Type dbContextType, IIocManager iocManager)
        {
            AutoRepositoryTypesAttribute autoRepositoryAttr = dbContextType.GetSingleAttributeOrNull<DapperAutoRepositoryTypeAttribute>()
                                                              ?? DapperAutoRepositoryTypes.Default;

            GenericRepositoryRegistrar.AutoRegistrarRepository(dbContextType, iocManager, autoRepositoryAttr);
        }
    }
}
