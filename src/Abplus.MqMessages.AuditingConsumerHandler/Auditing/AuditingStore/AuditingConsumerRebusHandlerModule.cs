using Abp.Modules;
using System.Reflection;

namespace Abp.Auditing.AuditingStore
{
    public class AuditingConsumerRebusHandlerModule : AbpModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<IAuditingConsumerRebusHandlerModuleConfig, AuditingConsumerRebusHandlerModuleConfig>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        public override void PostInitialize()
        {
            MqMessageAuditingStoreRebusHandler.Timer.Start();
        }

        public override void Shutdown()
        {
            MqMessageAuditingStoreRebusHandler.Timer.Stop();
        }
    }
}
