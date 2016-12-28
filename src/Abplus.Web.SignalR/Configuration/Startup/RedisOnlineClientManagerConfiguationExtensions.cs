using Abp.Configuration.Startup;
using Abp.RealTime;

namespace Abp.Configuration.Startup
{
    public static class RedisOnlineClientManagerConfiguationExtensions
    {
        public static IRedisOnlineClientManagerModuleConfig UseRedisOnlineClientManager(this IModuleConfigurations configurations)
        {
            return configurations.AbpConfiguration.GetOrCreate("Modules.Abplus.RedisOnlineClientManager", () => configurations.AbpConfiguration.IocManager.Resolve<IRedisOnlineClientManagerModuleConfig>());
        }
    }
}
