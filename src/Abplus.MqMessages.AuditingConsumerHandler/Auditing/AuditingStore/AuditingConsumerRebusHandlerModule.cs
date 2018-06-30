using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Abp.Modules;

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
