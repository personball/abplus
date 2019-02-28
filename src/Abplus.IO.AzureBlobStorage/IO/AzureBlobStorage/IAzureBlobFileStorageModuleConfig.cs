namespace Abp.IO.AzureBlobStorage
{
    public interface IAzureBlobFileStorageModuleConfig : IAzureBlobFileStorageConfig
    {
        bool UseSettingManager { get; }

        void ConfigAzureStorageUseSettingManager();

        IAzureBlobFileStorageModuleConfig ConfigAzureStorage();
        IAzureBlobFileStorageModuleConfig SetAccountName(string accountName);
        IAzureBlobFileStorageModuleConfig SetAccountKey(string accountKey);
        IAzureBlobFileStorageModuleConfig SetContainer(string container);
        IAzureBlobFileStorageModuleConfig UseEndpointSuffix(string endpointSuffix);
    }
}
