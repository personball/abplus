namespace Abp.IO.AzureBlobStorage
{
    public class AzureBlobFileStorageConfig : IAzureBlobFileStorageConfig
    {
        private readonly IAzureBlobFileStorageModuleConfig _moduleConfig;

        public AzureBlobFileStorageConfig(IAzureBlobFileStorageModuleConfig moduleConfig)
        {
            _moduleConfig = moduleConfig;
        }

        public string AccountName => _moduleConfig.AccountName;

        public string AccountKey => _moduleConfig.AccountKey;

        public string Container => _moduleConfig.Container;

        public string EndpointSuffix => _moduleConfig.EndpointSuffix;
    }
}
