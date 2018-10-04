using System;
using System.Web;
using Abp.Runtime.Security;
using Abp.Extensions;

namespace Abp.Web.SimpleCaptcha.VerificationCodeStores
{
    public class CookieVerificationCodeStore : IVerificationCodeStore
    {
        private readonly ISimpleCaptchaModuleConfig _config;
        public CookieVerificationCodeStore(ISimpleCaptchaModuleConfig config)
        {
            _config = config;
        }

        public void Clear(string storeKey)
        {
            Check.NotNullOrWhiteSpace(storeKey, nameof(storeKey));

            var valiCodeCookie = HttpContext.Current.Request.Cookies[storeKey];
            if (valiCodeCookie == null || valiCodeCookie.Value.IsNullOrWhiteSpace())
            {
                return;
            }

            valiCodeCookie.Expires = DateTime.UtcNow.AddDays(-1).ToLocalTime();
            HttpContext.Current.Response.Cookies.Add(valiCodeCookie);
        }

        public string Find(string storeKey)
        {
            var valiCodeCookie = HttpContext.Current.Request.Cookies[storeKey];
            if (valiCodeCookie == null || valiCodeCookie.Value.IsNullOrWhiteSpace())
            {
                return string.Empty;
            }

            if (_config.CookieCodeStoreSecretKey.IsNullOrWhiteSpace())
            {
                return valiCodeCookie.Value;
            }

            return SimpleStringCipher.Instance.Decrypt(valiCodeCookie.Value, _config.CookieCodeStoreSecretKey);
        }

        public void Save(string storeKey, string verificationCode)
        {
            var cookieValue = verificationCode;

            if (!_config.CookieCodeStoreSecretKey.IsNullOrWhiteSpace())
            {
                cookieValue = SimpleStringCipher.Instance.Encrypt(verificationCode, _config.CookieCodeStoreSecretKey);
            }

            var codeCookie = new HttpCookie(storeKey, cookieValue);
            codeCookie.Path = "/";
            codeCookie.HttpOnly = true;
            codeCookie.Expires = DateTime.UtcNow.AddMinutes(5).ToLocalTime();
            HttpContext.Current.Response.Cookies.Add(codeCookie);
        }
    }
}
