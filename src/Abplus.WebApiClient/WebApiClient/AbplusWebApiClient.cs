using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Extensions;
using Abp.Json;
using Castle.Core.Logging;

namespace Abp.WebApiClient
{
    public class AbplusWebApiClient : IAbplusWebApiClient, ITransientDependency
    {
        public static ConcurrentDictionary<string, HttpClient> clientDictionary { get; set; }

        public static int DefaultTimeoutInSeconds { get; set; }

        public string BaseUrl { get; set; }

        public TimeSpan Timeout { get; set; }

        public ILogger Logger { get; set; }

        public Collection<Cookie> Cookies { get; private set; }

        public ICollection<NameValue> RequestHeaders { get; private set; }

        public ICollection<NameValue> ResponseHeaders { get; private set; }

        static AbplusWebApiClient()
        {
            clientDictionary = new ConcurrentDictionary<string, HttpClient>();
            DefaultTimeoutInSeconds = 90;
        }

        public AbplusWebApiClient()
        {
            Timeout = TimeSpan.FromSeconds(DefaultTimeoutInSeconds);
            Logger = NullLogger.Instance;
        }

        public async Task GetAsync(string url, int? timeout = null)
        {
            await GetAsync<object>(url, timeout);
        }

        public async Task<TResult> GetAsync<TResult>(string url, int? timeout = null)
        {
            return await RequestAsync<TResult>(url, null, timeout, HttpRequestMethod.GET);
        }

        public async Task PostAsync(string url, int? timeout = null)
        {
            await PostAsync<object>(url, timeout);
        }

        public async Task PostAsync(string url, object input, int? timeout = null)
        {
            await PostAsync<object>(url, input, timeout);
        }

        public async Task<TResult> PostAsync<TResult>(string url, int? timeout = null) where TResult : class
        {
            return await PostAsync<TResult>(url, null, timeout);
        }

        public async Task<TResult> PostAsync<TResult>(string url, object input, int? timeout = null) where TResult : class
        {
            return await RequestAsync<TResult>(url, input, timeout, HttpRequestMethod.POST);
        }

        private async Task<TResult> RequestAsync<TResult>(string url, object input, int? timeoutInMilliseconds = null, HttpRequestMethod method = HttpRequestMethod.POST)
        {
            //httpclient 实例
            var uri = new Uri(url);
            timeoutInMilliseconds = timeoutInMilliseconds.HasValue ? timeoutInMilliseconds : DefaultTimeoutInSeconds * 1000;
            var dictKey = uri.Scheme + "|" + uri.Host + "|" + uri.Port + "|" + timeoutInMilliseconds;
            dictKey = dictKey.ToLower();
            HttpClient client;
            if (!clientDictionary.Keys.Contains(dictKey) || clientDictionary[dictKey] == null)
            {
                client = new HttpClient();
                client.Timeout = TimeSpan.FromMilliseconds(timeoutInMilliseconds.Value);
                if (method == HttpRequestMethod.POST)
                {
                    //POST
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                }

                if (clientDictionary.Keys.Contains(dictKey))
                {
                    clientDictionary.TryUpdate(dictKey, client, null);
                }
                else
                {
                    clientDictionary.TryAdd(dictKey, client);
                }

                var sp = ServicePointManager.FindServicePoint(uri);
                sp.ConnectionLeaseTimeout = (int)Math.Floor(Timeout.TotalMilliseconds * 2);
            }
            else
            {
                client = clientDictionary[dictKey];
            }

            if (input == null)
            {
                input = new object();
            }

            if (method == HttpRequestMethod.GET)
            {
                //GET
                using (var response = await client.GetAsync(url))
                {
                    var info = string.Empty;
                    var strReturn = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        info = string.Format("RemoteApiCallFailed\r\n GET Url:{0}\r\n Response:{1}", url, strReturn);
                        Logger.Error(info);
                        throw new AbplusWebApiClientRemoteCallException(info);
                    }

                    info = string.Format("RemoteApiCallSuccess\r\n GET Url:{0}\r\n Response:{1}", url, strReturn);
                    Logger.Info(info);

                    return strReturn.ToObject<TResult>();
                }
            }
            else
            {
                var strInput = input.ToJsonString();
                using (var requestContent = new StringContent(strInput, Encoding.UTF8, "application/json"))
                {
                    using (var response = await client.PostAsync(url, requestContent))
                    {
                        var info = string.Empty;
                        var strReturn = await response.Content.ReadAsStringAsync();

                        if (!response.IsSuccessStatusCode)
                        {
                            info = string.Format("RemoteApiCallFailed\r\n POST Url:{0}\r\n,Input:{1}\r\n Response:{2}", url, input.ToJsonString(), strReturn);
                            throw new AbplusWebApiClientRemoteCallException(info);
                        }

                        info = info = string.Format("RemoteApiCallSuccess\r\n POST Url:{0}\r\n,Input:{1}\r\n Response:{2}", url, input.ToJsonString(), strReturn);
                        Logger.Info(info);

                        return strReturn.ToObject<TResult>();
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public enum HttpRequestMethod
        {
            /// <summary>
            /// HttpPost
            /// </summary>
            POST = 0,
            /// <summary>
            ///  HttpGET
            /// </summary>
            GET = 1
        }
    }
}
