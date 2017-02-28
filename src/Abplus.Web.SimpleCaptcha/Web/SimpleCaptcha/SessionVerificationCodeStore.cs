using System;
using System.Web;

namespace Abp.Web.SimpleCaptcha
{
    public class SessionVerificationCodeStore : IVerificationCodeStore
    {
        public string Find(string storeKey)
        {
            if (string.IsNullOrWhiteSpace(storeKey))
            {
                throw new ArgumentNullException(nameof(storeKey), $"{nameof(storeKey)} should not be empty!");
            }

            if (HttpContext.Current.Session[storeKey] == null)
            {
                return string.Empty;
            }

            return HttpContext.Current.Session[storeKey].ToString();
        }

        public void Save(string storeKey, string verificationCode)
        {
            if (string.IsNullOrWhiteSpace(storeKey))
            {
                throw new ArgumentNullException(nameof(storeKey), $"{nameof(storeKey)} should not be empty!");
            }

            if (string.IsNullOrWhiteSpace(verificationCode))
            {
                throw new ArgumentNullException(nameof(verificationCode), $"{nameof(verificationCode)} should not be empty!");
            }

            HttpContext.Current.Session[storeKey] = verificationCode;
        }
    }
}
