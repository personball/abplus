using System.IO;
using System.Threading.Tasks;
using Abp.Dependency;

namespace Abp.IO
{
    public interface IFileStorage : ITransientDependency
    {
        Task<string> Save(Stream source, string fileName, string subPath = null);

        Task<string> Save(byte[] source, string fileName, string subPath = null);

        Task Delete(string fileName, string subPath = null);

        Task<byte[]> ReadAsBytes(string fileName, string subPath = null);
    }
}
