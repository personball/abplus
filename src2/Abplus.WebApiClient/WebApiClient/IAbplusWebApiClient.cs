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

        Task<TResult> GetAsync<TResult>(string url, int? timeout = null, bool? hasErrorCodeIf500 = null);

        Task<TResult> PostAsync<TResult>(string url, object input, int? timeout = null, bool? hasErrorCodeIf500 = null);
    }
}
