using System.Threading.Tasks;
using Abp.WebApi.Client;

namespace Abp.WebApiClient
{
    /// <summary>
    /// 扩展IAbpWebApiClient
    /// </summary>
    public interface IAbplusWebApiClient : IAbpWebApiClient
    {
        Task GetAsync(string url, int? timeout = null);

        Task<TResult> GetAsync<TResult>(string url, int? timeout = null);
    }
}
