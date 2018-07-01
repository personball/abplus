using System.Reflection;
using Abp.Dependency;
using Abp.Modules;

namespace Abp.RealTime
{
    public class RedisOnlineClientManagerModule : AbpModule
    {
        public override void PreInitialize()
        {
            //base.PreInitialize();
            IocManager.Register<IRedisOnlineClientManagerModuleConfig, RedisOnlineClientManagerModuleConfig>();
            IocManager.Register<IOnlineClientManager, RedisOnlineClientManager>(DependencyLifeStyle.Singleton);
        }

        public override void Initialize()
        {
            //base.Initialize();
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }

}
