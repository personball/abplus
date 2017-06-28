using System;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.EntityFramework.Extensions;
using Abp.Reflection.Extensions;

namespace Abp.EntityFramework.Repositories
{
    public class EntityFrameworkGenericBatchRepositoryRegistrar
    {
        public static void RegisterForDbContext(Type dbContextType, IIocManager iocManager)
        {
            AutoRepositoryTypesAttribute autoRepositoryAttr = dbContextType.GetSingleAttributeOrNull<BatchAutoRepositoryTypesAttribute>()
                                                  ?? BatchAutoRepositoryTypes.Default;

            GenericRepositoryRegistrar.AutoRegistrarRepository(dbContextType, iocManager, autoRepositoryAttr);
        }
    }
}
