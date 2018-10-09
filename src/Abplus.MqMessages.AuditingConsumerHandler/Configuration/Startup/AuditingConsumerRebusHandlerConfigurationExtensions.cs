using Abp.Auditing.AuditingStore;

namespace Abp.Configuration.Startup
{
    public static class AuditingConsumerRebusHandlerConfigurationExtensions
    {
        public static IAuditingConsumerRebusHandlerModuleConfig AuditingConsumer(this IModuleConfigurations configurations)
        {
            return configurations.AbpConfiguration.GetOrCreate("Modules.Abplus.AuditingConsumer", () => configurations.AbpConfiguration.IocManager.Resolve<IAuditingConsumerRebusHandlerModuleConfig>());
        }
    }
}
