using Abp.IO.AzureBlobStorage;

namespace Abp.Configuration.Startup
{
    public static  class AzureBlobFileStorageConfigurationExtensions
    {
        public static IAzureBlobFileStorageModuleConfig UseAzureBlobFileStorage(this IModuleConfigurations configurations)
        {
            return configurations.AbpConfiguration.GetOrCreate("Modules.Abplus.AzureBlobFileStorage", 
                () => configurations.AbpConfiguration.IocManager.Resolve<IAzureBlobFileStorageModuleConfig>());
        }
    }
}
