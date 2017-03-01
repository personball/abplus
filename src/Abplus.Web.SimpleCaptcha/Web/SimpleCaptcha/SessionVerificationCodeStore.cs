using System;
using System.Web;
using Abp.UI;

namespace Abp.Web.SimpleCaptcha
{
    public class SessionVerificationCodeStore : IVerificationCodeStore
    {
        public void Clear(string storeKey)
        {
            Check.NotNullOrWhiteSpace(storeKey, nameof(storeKey));

            if (HttpContext.Current.Session == null)
            {
                return;
            }

            HttpContext.Current.Session.Remove(storeKey);
        }

        public string Find(string storeKey)
        {
            Check.NotNullOrWhiteSpace(storeKey, nameof(storeKey));

            if (HttpContext.Current.Session == null)
            {
                throw new UserFriendlyException("HttpContext.Current.Session is null! ");
            }

            if (HttpContext.Current.Session[storeKey] == null)
            {
                return string.Empty;
            }

            return HttpContext.Current.Session[storeKey].ToString();
        }

        public void Save(string storeKey, string verificationCode)
        {
            Check.NotNullOrWhiteSpace(storeKey, nameof(storeKey));
            Check.NotNullOrWhiteSpace(verificationCode, nameof(verificationCode));

            if (HttpContext.Current.Session == null)
            {
                throw new UserFriendlyException("HttpContext.Current.Session is null! ");
            }

            HttpContext.Current.Session[storeKey] = verificationCode;
        }
    }
}
