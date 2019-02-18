namespace Abp.IO.AzureBlobStorage
{
    public interface IAzureBlobFileStorageConfig
    {
        string AccountName { get;  }
        string AccountKey { get; }
        string Container { get;  }
        // string ThumbnailContainer { get; }
        string EndpointSuffix { get; }
    }
}
