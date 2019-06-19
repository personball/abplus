using System.Reflection;
using Abp.Localization;
using Abp.Modules;

namespace Abp.IO.AliyunOSSStorage
{
    [DependsOn(typeof(AbpKernelModule))]
    public class AliyunOSSStorageModule : AbpModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<IAliyunOSSStorageModuleConfiguration, AliyunOSSStorageModuleConfiguration>();
        }
        public override void Initialize()
        {
            IocManager.Register<IFileStorage, AliyunOSSStorage>();
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            AliyunOSSStorageLocalizationConfigurer.Configure(Configuration.Localization);
        }
    }
}
