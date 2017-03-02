using System;
using Abp.Extensions;
using Abp.Runtime.Caching;
using Castle.Core.Logging;

namespace Abp.Web.SimpleCaptcha.VerificationCodeStores
{
    public class CacheVerificationCodeStore : IVerificationCodeStore
    {
        private readonly ICacheManager _cacheManager;
        private const string DefaultCacheName = "CacheVerificationCodeStore";

        public ILogger Logger { get; set; }

        public CacheVerificationCodeStore(ICacheManager cacheManager)
        {
            Logger = NullLogger.Instance;
            _cacheManager = cacheManager;
        }

        public void Clear(string storeKey)
        {
            Check.NotNullOrWhiteSpace(storeKey, nameof(storeKey));
            try
            {
                _cacheManager.GetCache(DefaultCacheName).Remove(storeKey);
            }
            catch (Exception ex)
            {
                Logger.Warn(ex.Message, ex);
            }
        }

        public string Find(string storeKey)
        {
            Check.NotNullOrWhiteSpace(storeKey, nameof(storeKey));

            var code = _cacheManager.GetCache(DefaultCacheName).GetOrDefault<string, string>(storeKey);

            if (code.IsNullOrWhiteSpace())
            {
                return string.Empty;
            }

            return code;
        }

        public void Save(string storeKey, string verificationCode)
        {
            Check.NotNullOrWhiteSpace(storeKey, nameof(storeKey));
            Check.NotNullOrWhiteSpace(verificationCode, nameof(verificationCode));

            _cacheManager.GetCache(DefaultCacheName).Set(storeKey, verificationCode, null, TimeSpan.FromMinutes(20));
        }
    }
}
