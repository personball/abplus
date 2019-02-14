using System;
using System.IO;
using System.Threading.Tasks;
using Abp.Configuration;

namespace Abp.IO.LocalFileSystem
{
    /// <summary>
    /// 本地存储，和部署情况相关，只能针对Application配置
    /// TODO 跨平台兼容？
    /// </summary>
    public class LocalFileSystemStorage : IFileStorage
    {
        private readonly ILocalFileSystemStorageConfig _storageConfig;

        public LocalFileSystemStorage(ILocalFileSystemStorageConfig storageConfig)
        {
            _storageConfig = storageConfig;
        }
        public Task Delete(string fileName, string subPath = null)
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> ReadAsBytes(string fileName, string subPath = null)
        {
            throw new NotImplementedException();
        }

        public Task<string> Save(Stream source, string fileName, string subPath = null)
        {
            throw new NotImplementedException();
        }

        public Task<string> Save(byte[] source, string fileName, string subPath = null)
        {
            throw new NotImplementedException();
        }
    }
}
