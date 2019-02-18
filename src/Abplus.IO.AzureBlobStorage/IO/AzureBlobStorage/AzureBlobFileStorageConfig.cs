using Abp.Configuration;

namespace Abp.IO.AzureBlobStorage
{
    /// <summary>
    /// 通过SettingManager可以实现每个Tenant配置自己的Azure存储
    /// </summary>
    public class AzureBlobFileStorageConfig : IAzureBlobFileStorageConfig
    {
        protected SettingManager SettingManager;

        public AzureBlobFileStorageConfig(SettingManager settingManager)
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
