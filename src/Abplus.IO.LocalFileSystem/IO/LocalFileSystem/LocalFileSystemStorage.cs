using System.IO;
using System.Threading.Tasks;
using Abp.Extensions;
using Abp.IO.Extensions;
#pragma warning disable CS1998
namespace Abp.IO.LocalFileSystem
{
    /// <summary>
    /// 本地存储，和部署情况相关，只能针对Application配置
    /// 先实现win系统
    /// TODO@personball 跨平台兼容
    /// </summary>
    public class LocalFileSystemStorage : IFileStorage
    {
        private readonly ILocalFileSystemStorageConfig _config;

        public LocalFileSystemStorage(ILocalFileSystemStorageConfig config)
        {
            _config = config;
        }

        public async Task Delete(string fileName, string subPath = null)
        {
            var filePath = $"{_config.StoreRootDirectory.EnsureEndsWith('\\')}{subPath.EnsureEndsWith('\\')}{fileName}";

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public async Task<byte[]> ReadAsBytes(string fileName, string subPath = null)
        {
            var filePath = $"{_config.StoreRootDirectory.EnsureEndsWith('\\')}{subPath.EnsureEndsWith('\\')}{fileName}";

            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                return fs.GetAllBytes();
            }
        }

        public async Task<string> Save(Stream source, string fileName, string subPath = null)
        {
            if (!subPath.IsNullOrWhiteSpace())
            {
                subPath = subPath.EnsureEndsWith('\\');
            }

            var path = $"{_config.StoreRootDirectory.EnsureEndsWith('\\')}{subPath}";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            using (var fs = new FileStream($"{path}{fileName}", FileMode.Create))
            {
                await source.CopyToAsync(fs);
            }

            return AbsoluteAccessUri(_config, subPath.Replace('\\', '/'), fileName);
        }

        private string AbsoluteAccessUri(ILocalFileSystemStorageConfig config, string subPath, string fileName)
        {
            return $"{config.AccessUriRootPath.EnsureEndsWith('/')}{subPath}{fileName}";
        }

        public async Task<string> Save(byte[] source, string fileName, string subPath = null)
        {
            if (!subPath.IsNullOrWhiteSpace())
            {
                subPath = subPath.EnsureEndsWith('\\');
            }

            var path = $"{_config.StoreRootDirectory.EnsureEndsWith('\\')}{subPath}";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            File.WriteAllBytes($"{path}{fileName}", source);

            return AbsoluteAccessUri(_config, subPath.Replace('\\', '/'), fileName);
        }
    }
}
#pragma warning restore CS1998
