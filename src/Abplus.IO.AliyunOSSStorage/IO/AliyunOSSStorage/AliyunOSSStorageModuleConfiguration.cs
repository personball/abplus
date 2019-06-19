namespace Abp.IO.AliyunOSSStorage
{
    public class AliyunOSSStorageModuleConfiguration : IAliyunOSSStorageModuleConfiguration
    {
        public string AccessKeyId { get; private set; }

        public string AccessKeySecret { get; private set; }

        public string Endpoint { get; private set; }

        public string BucketName { get; private set; }

        public string UriPrefix { get; private set; }

        public IAliyunOSSStorageModuleConfiguration SetAccessKeyId(string id)
        {
            AccessKeyId = id;
            return this;
        }

        public IAliyunOSSStorageModuleConfiguration SetAccessKeySecret(string secret)
        {
            AccessKeySecret = secret;
            return this;
        }

        public IAliyunOSSStorageModuleConfiguration SetBucketName(string bucketName)
        {
            BucketName = bucketName;
            return this;
        }

        public IAliyunOSSStorageModuleConfiguration SetEndpoint(string endpoint)
        {
            Endpoint = endpoint;
            return this;
        }

        public IAliyunOSSStorageModuleConfiguration WithUriPrefix(string prefix)
        {
            UriPrefix = prefix;
            return this;
        }
    }
}
