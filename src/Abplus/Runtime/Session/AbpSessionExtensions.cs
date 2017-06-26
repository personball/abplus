using Abp.Configuration.Startup;
using Abp.MultiTenancy;
using Abp.Runtime;
using Abp.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Abp.Runtime.Session
{
    /// <summary>
    /// 通过扩展方法来对AbpSession进行扩展
    /// </summary>
    public static class AbpSessionExtensions
    {
        public static string Email(this IAbpSession session) => GetClaimValue(ClaimTypes.Email);
        public static string Surname(this IAbpSession session) => GetClaimValue(ClaimTypes.Surname);
        public static string Name(this IAbpSession session) => GetClaimValue(ClaimTypes.Name);
        public static string UserName(this IAbpSession session) => GetClaimValue(AbplusConsts.ClaimTypes.UserName);
        public static string FullName(this IAbpSession session) => GetClaimValue(AbplusConsts.ClaimTypes.FullName);
        private static string GetClaimValue(string claimType)
        {
            var claimsPrincipal = DefaultPrincipalAccessor.Instance.Principal;

            var claim = claimsPrincipal?.Claims.FirstOrDefault(c => c.Type == claimType);
            if (string.IsNullOrEmpty(claim?.Value))
                return null;

            return claim.Value;
        }
    }
}
