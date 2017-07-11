using Abp.Auditing;
using Abp.Auditing.AuditingStore;

namespace Abp.Configuration.Startup
{
    public static class MqMessageAuditingStoreConfigurationExtensions
    {
        public static void UseMqMessageAuditingStore(this IModuleConfigurations configurations)
        {
            configurations.AbpConfiguration.IocManager.Register<IAuditingStore, MqMessageAuditingStore>();
        }
    }
}
