using System;
using System.IO;
using System.Threading.Tasks;
using Abp.Extensions;
using Abp.IO.Extensions;
using Castle.Core.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Abp.IO.AzureBlobStorage
{
    public class AzureBlobFileStorage : IFileStorage
    {
        private readonly IAzureBlobFileStorageConfig _config;
        protected ILogger Logger { get; set; }

        public AzureBlobFileStorage(IAzureBlobFileStorageConfig config)
        {
            _config = config;
            Logger = NullLogger.Instance;
        }

        public async Task Delete(string fileName, string subPath = null)
        {
            if (!subPath.IsNullOrWhiteSpace())
            {
                subPath = subPath.EnsureEndsWith('/');
            }

            var container = GetCloudBlobContainer(_config);
            // Get the reference to the block blob from the container
            CloudBlockBlob blockBlob = container.GetBlockBlobReference($"{subPath}{fileName}");

            try
            {
                await blockBlob.DeleteIfExistsAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<byte[]> ReadAsBytes(string fileName, string subPath = null)
        {
            if (!subPath.IsNullOrWhiteSpace())
            {
                subPath = subPath.EnsureEndsWith('/');
            }

            var container = GetCloudBlobContainer(_config);
            // Get the reference to the block blob from the container
            CloudBlockBlob blockBlob = container.GetBlockBlobReference($"{subPath}{fileName}");

            try
            {
                using (var rs = blockBlob.OpenRead())
                {
                    return rs.GetAllBytes();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<string> Save(Stream source, string fileName, string subPath = null)
        {
            if (!subPath.IsNullOrWhiteSpace())
            {
                subPath = subPath.EnsureEndsWith('/');
            }

            await UploadFileToStorage(source, $"{subPath}{fileName}", _config);

            return AbsoluteAccessPath(_config, $"{subPath}{fileName}");
        }

        public async Task<string> Save(byte[] source, string fileName, string subPath = null)
        {
            if (!subPath.IsNullOrWhiteSpace())
            {
                subPath = subPath.EnsureEndsWith('/');
            }

            await UploadFileToStorage(source, $"{subPath}{fileName}", _config);

            return AbsoluteAccessPath(_config, $"{subPath}{fileName}");
        }

        private static string AbsoluteAccessPath(IAzureBlobFileStorageConfig config, string relativePathAndFileName)
        {
            return $"https://{config.AccountName}.blob.{config.EndpointSuffix}/{config.Container}/{relativePathAndFileName}";
        }

        private static async Task<bool> UploadFileToStorage(byte[] fileBytes, string fileName, IAzureBlobFileStorageConfig _storageConfig)
        {
            var container = GetCloudBlobContainer(_storageConfig);
            // Get the reference to the block blob from the container
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);

            // Upload the file
            await blockBlob.UploadFromByteArrayAsync(fileBytes, 0, fileBytes.Length);

            return await Task.FromResult(true);
        }

        private static async Task<bool> UploadFileToStorage(Stream fileStream, string fileName, IAzureBlobFileStorageConfig _storageConfig)
        {
            var container = GetCloudBlobContainer(_storageConfig);
            // Get the reference to the block blob from the container
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);

            // Upload the file
            await blockBlob.UploadFromStreamAsync(fileStream);

            return await Task.FromResult(true);
        }

        private static CloudBlobContainer GetCloudBlobContainer(IAzureBlobFileStorageConfig storageConfig)
        {
            // Create storagecredentials object by reading the values from the configuration (appsettings.json)
            StorageCredentials storageCredentials = new StorageCredentials(storageConfig.AccountName, storageConfig.AccountKey);

            // Create cloudstorage account by passing the storagecredentials
            CloudStorageAccount storageAccount = new CloudStorageAccount(storageCredentials, storageConfig.EndpointSuffix, true);

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Get reference to the blob container by passing the name by reading the value from the configuration (appsettings.json)
            return blobClient.GetContainerReference(storageConfig.Container);
        }

    }
}
