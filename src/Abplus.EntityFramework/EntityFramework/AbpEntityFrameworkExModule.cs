using System.Reflection;
using Abp.Collections.Extensions;
using Abp.EntityFramework.Repositories;
using Abp.Modules;
using Abp.Reflection;

namespace Abp.EntityFramework
{
    [DependsOn(
        typeof(AbpEntityFrameworkModule))]
    public class AbpEntityFrameworkExModule : AbpModule
    {
        private readonly ITypeFinder _typeFinder;
        public AbpEntityFrameworkExModule(ITypeFinder typeFinder)
        {
            _typeFinder = typeFinder;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            
            RegisterGenericRepositories();
        }

        private void RegisterGenericRepositories()
        {
            var dbContextTypes =
                _typeFinder.Find(type =>
                    type.IsPublic &&
                    !type.IsAbstract &&
                    type.IsClass &&
                    typeof(AbpDbContext).IsAssignableFrom(type)
                    );

            if (dbContextTypes.IsNullOrEmpty())
            {
                return;
            }

            foreach (var item in dbContextTypes)
            {
                EntityFrameworkGenericBatchRepositoryRegistrar.RegisterForDbContext(item, IocManager);

                //var type = typeof(IAbpBatchRunner<>).MakeGenericType(item);
                //var implType = typeof(AbpSqlServerBatchRunner<>).MakeGenericType(item);
                //IocManager.RegisterIfNot(type, implType, DependencyLifeStyle.Transient);
            }
        }
    }
}
