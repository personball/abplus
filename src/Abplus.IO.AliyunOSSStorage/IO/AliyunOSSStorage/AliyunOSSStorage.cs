using System;
using System.IO;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Extensions;
using Abp.IO.Extensions;
using Aliyun.OSS;
using Castle.Core.Logging;

namespace Abp.IO.AliyunOSSStorage
{
    public class AliyunOSSStorage : IFileStorage
    {
        private static Lazy<OssClient> _client = new Lazy<OssClient>(InitClient, false);

        private static OssClient InitClient()
        {
            var c = IocManager.Instance.Resolve<IAliyunOSSStorageModuleConfiguration>();
            return new OssClient(c.Endpoint, c.AccessKeyId, c.AccessKeySecret);
        }

        private readonly IAliyunOSSStorageModuleConfiguration _config;

        protected ILogger Logger { get; set; }

        public AliyunOSSStorage(IAliyunOSSStorageModuleConfiguration config)
        {
            _config = config;
            Logger = NullLogger.Instance;
        }

        public Task Delete(string fileName, string subPath = null)
        {
            if (!subPath.IsNullOrWhiteSpace())
            {
                subPath = subPath.EnsureEndsWith('/');
            }

            _client.Value.DeleteObject(_config.BucketName, $"{subPath}{fileName}");
            return Task.FromResult(0);
        }

        public Task<byte[]> ReadAsBytes(string fileName, string subPath = null)
        {
            if (!subPath.IsNullOrWhiteSpace())
            {
                subPath = subPath.EnsureEndsWith('/');
            }

            var ossObj = _client.Value.GetObject(_config.BucketName, $"{subPath}{fileName}");
            try
            {
                using (var rs = ossObj.Content)
                {
                    var bytes = rs.GetAllBytes();
                    return Task.FromResult(bytes);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public Task<string> Save(Stream source, string fileName, string subPath = null)
        {
            if (!subPath.IsNullOrWhiteSpace())
            {
                subPath = subPath.EnsureEndsWith('/');
            }

            _client.Value.PutObject(_config.BucketName, $"{subPath}{fileName}", source);

            if (!_config.UriPrefix.IsNullOrWhiteSpace())
            {
                return Task.FromResult($"{_config.UriPrefix.EnsureEndsWith('/')}{subPath}{fileName}");
            }

            return Task.FromResult($"{subPath}{fileName}");
        }

        public Task<string> Save(byte[] source, string fileName, string subPath = null)
        {
            if (!subPath.IsNullOrWhiteSpace())
            {
                subPath = subPath.EnsureEndsWith('/');
            }

            var stream = new MemoryStream(source);

            _client.Value.PutObject(_config.BucketName, $"{subPath}{fileName}", stream);

            if (!_config.UriPrefix.IsNullOrWhiteSpace())
            {
                return Task.FromResult($"{_config.UriPrefix.EnsureEndsWith('/')}{subPath}{fileName}");
            }

            return Task.FromResult($"{subPath}{fileName}");
        }
    }
}
