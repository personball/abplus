using System;
using System.Threading.Tasks;
using Abp.Extensions;
using Abp.Runtime.Caching;

namespace Abp.MqMessages.MessageTrackers
{
    public class RedisStoreMessageTracker : IMessageTracker
    {
        private const string CacheKey = "Abplus.MqMessages.MessageTrackers.RedisStoreMessageTracker";
        private readonly ICacheManager _cacheManager;

        public RedisStoreMessageTracker(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public async Task<bool> HasProcessed(string processId)
        {
            var value = await _cacheManager.GetCache<string, string>(CacheKey).GetOrDefaultAsync(processId);
            return !value.IsNullOrWhiteSpace();
        }

        public async Task MarkAsProcessed(string processId)
        {
            await _cacheManager.GetCache<string, string>(CacheKey).SetAsync(processId, "1", TimeSpan.FromDays(30));
        }
    }
}
