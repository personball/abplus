using System.Reflection;
using Abp.IO.AzureBlobStorage.Configuration;
using Abp.Localization;
using Abp.Modules;

namespace Abp.IO.AzureBlobStorage
{
    /// <summary>
    /// TODO 是否由本模块自行实现相关setting设置接口？
    /// </summary>
    [DependsOn(typeof(AbpKernelModule))]//依赖SettingManager
    public class AzureBlobFileStorageModule : AbpModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<IAzureBlobFileStorageModuleConfig, AzureBlobFileStorageModuleConfig>();
        }

        public override void Initialize()
        {
            var moduleConfig = IocManager.Resolve<IAzureBlobFileStorageModuleConfig>();
            if (moduleConfig.UseSettingManager)
            {
                IocManager.Register<IAzureBlobFileStorageConfig, AzureBlobFileStorageSetting>();
            }
            else
            {
                IocManager.Register<IAzureBlobFileStorageConfig, AzureBlobFileStorageConfig>();
            }

            IocManager.Register<IFileStorage, AzureBlobFileStorage>();
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            AzureBlobStorageLocalizationConfigurer.Configure(Configuration.Localization);
        }
    }
}
