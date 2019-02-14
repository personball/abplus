namespace Abp.IO.LocalFileSystem
{
    public interface ILocalFileSystemStorageConfig
    {
        string StoreRootDirectory { get; }

        string AccessUriRootPath { get; }
    }
}
