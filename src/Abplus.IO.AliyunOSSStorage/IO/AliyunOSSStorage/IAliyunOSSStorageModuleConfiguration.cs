namespace Abp.IO.AliyunOSSStorage
{
    public interface IAliyunOSSStorageModuleConfiguration
    {
        string AccessKeyId { get; }
        string AccessKeySecret { get; }
        string Endpoint { get; }
        string BucketName { get; }
        string UriPrefix { get; }

        IAliyunOSSStorageModuleConfiguration SetAccessKeyId(string id);
        IAliyunOSSStorageModuleConfiguration SetAccessKeySecret(string secret);
        IAliyunOSSStorageModuleConfiguration SetEndpoint(string endpoint);
        IAliyunOSSStorageModuleConfiguration SetBucketName(string bucketName);
        IAliyunOSSStorageModuleConfiguration WithUriPrefix(string prefix);
    }
}
