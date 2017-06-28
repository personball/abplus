using System;
using Abp.Domain.Repositories;

namespace Abp.EntityFramework
{
    public class BatchAutoRepositoryTypesAttribute : AutoRepositoryTypesAttribute
    {
        public BatchAutoRepositoryTypesAttribute(
            Type repositoryInterface,
            Type repositoryInterfaceWithPrimaryKey,
            Type repositoryImplementation,
            Type repositoryImplementationWithPrimaryKey)
            : base(repositoryInterface, repositoryInterfaceWithPrimaryKey, repositoryImplementation, repositoryImplementationWithPrimaryKey)
        {
        }
    }
}
