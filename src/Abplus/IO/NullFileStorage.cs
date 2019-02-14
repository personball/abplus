using System.IO;
using System.Threading.Tasks;

namespace Abp.IO
{
    public class NullFileStorage : IFileStorage
    {
        public static NullFileStorage Instance { get; } = new NullFileStorage();

        public Task Delete(string fileName, string subPath = null)
        {
            return Task.FromResult(0);
        }

        public Task<byte[]> ReadAsBytes(string fileName, string subPath = null)
        {
            return Task.FromResult(new byte[0]);
        }

        public Task<string> Save(Stream source, string fileName, string subPath = null)
        {
            return Task.FromResult("");
        }

        public Task<string> Save(byte[] source, string fileName, string subPath = null)
        {
            return Task.FromResult("");
        }
    }
}
