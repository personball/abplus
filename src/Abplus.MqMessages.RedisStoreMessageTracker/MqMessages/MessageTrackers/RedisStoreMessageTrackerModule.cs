using Abp.Modules;
using Abp.Runtime.Caching.Redis;

namespace Abp.MqMessages.MessageTrackers
{
    [DependsOn(typeof(AbpRedisCacheModule))]
    public class RedisStoreMessageTrackerModule : AbpModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<IMessageTracker, RedisStoreMessageTracker>();
        }

        public override void Initialize()
        {
            //IocManager.RegisterIfNot<ISerializer, NewtonsoftSerializer>(DependencyLifeStyle.Singleton);
            //IocManager.RegisterIfNot<ICacheClient, StackExchangeRedisCacheClient>(DependencyLifeStyle.Singleton);
        }
    }
}
