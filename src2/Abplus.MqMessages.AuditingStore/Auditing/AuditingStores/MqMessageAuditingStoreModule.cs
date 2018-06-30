using Abp.Modules;
using Abp.MqMessages.Publishers;

namespace Abp.Auditing.AuditingStores
{
    [DependsOn(typeof(RebusRabbitMqPublisherCoreModule))]
    public class MqMessageAuditingStoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<IAuditingStore, MqMessageAuditingStore>();
        }
    }
}
