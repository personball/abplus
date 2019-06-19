using Abp.IO.AliyunOSSStorage;

namespace Abp.Configuration.Startup
{
    public static class AliyunOSSStorageConfigurationExtensions
    {
        public static IAliyunOSSStorageModuleConfiguration UseAliyunOSSStorage(this IModuleConfigurations configurations)
        {
            return configurations.AbpConfiguration.GetOrCreate("Modules.Abplus.AliyunOSSStorage",
                () => configurations.AbpConfiguration.IocManager.Resolve<IAliyunOSSStorageModuleConfiguration>());
        }
    }
}
