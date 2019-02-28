namespace Abp.IO.AzureBlobStorage
{
    public class AzureBlobFileStorageModuleConfig : IAzureBlobFileStorageModuleConfig
    {
        public AzureBlobFileStorageModuleConfig()
        {
            UseSettingManager = false;
        }

        #region AzureBlobStorageConfig

        public string AccountName { get; private set; }

        public string AccountKey { get; private set; }

        public string Container { get; private set; }

        public string EndpointSuffix { get; private set; }

        #endregion

        public bool UseSettingManager { get; private set; }

        public IAzureBlobFileStorageModuleConfig ConfigAzureStorage()
        {
            UseSettingManager = false;
            return this;
        }

        public void ConfigAzureStorageUseSettingManager()
        {
            UseSettingManager = true;
        }

        public IAzureBlobFileStorageModuleConfig SetAccountKey(string accountKey)
        {
            AccountKey = accountKey;
            return this;
        }

        public IAzureBlobFileStorageModuleConfig SetAccountName(string accountName)
        {
            AccountName = accountName;
            return this;
        }

        public IAzureBlobFileStorageModuleConfig SetContainer(string container)
        {
            Container = container;
            return this;
        }

        public IAzureBlobFileStorageModuleConfig UseEndpointSuffix(string endpointSuffix)
        {
            EndpointSuffix = endpointSuffix;
            return this;
        }
    }
}
