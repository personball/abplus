using Abp.Configuration;

namespace Abp.IO.AzureBlobStorage.Configuration
{
    /// <summary>
    /// use SettingManager to config azure blob storage per tenant
    /// </summary>
    public class AzureBlobFileStorageSetting : IAzureBlobFileStorageConfig
    {
        protected SettingManager SettingManager;

        public AzureBlobFileStorageSetting(SettingManager settingManager)
        {
            SettingManager = settingManager;
        }

        public string AccountName
        {
            get
            {
                return SettingManager.GetSettingValue(AzureBlobFileStorageSettingNames.AccountName);
            }
        }

        public string AccountKey
        {
            get
            {
                return SettingManager.GetSettingValue(AzureBlobFileStorageSettingNames.AccountKey);
            }
        }

        public string Container
        {

            get
            {
                return SettingManager.GetSettingValue(AzureBlobFileStorageSettingNames.Container);
            }
        }

        public string EndpointSuffix
        {
            get
            {
                return SettingManager.GetSettingValue(AzureBlobFileStorageSettingNames.EndpointSuffix);
            }
        }
    }
}
