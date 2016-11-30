using Abp.Events.Consumer.RebusRabbitMq;

namespace Abp.Configuration.Startup
{
    public static class AbpRebusRabbitMqConsumerConfigurationExtensions
    {
        public static IAbpRebusRabbitMqConsumerModuleConfig AbpRebusRabbitMqConsumer(this IModuleConfigurations configurations)
        {
            return configurations.AbpConfiguration.GetOrCreate("Modules.Abp.RebusRabbitMqConsumer", () => configurations.AbpConfiguration.IocManager.Resolve<IAbpRebusRabbitMqConsumerModuleConfig>());
        }
    }
}
