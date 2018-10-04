using Abp.Modules;

namespace Abp.MqMessages.Publishers
{
    public class RebusRabbitMqPublisherCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<IMqMessagePublisher, RebusRabbitMqPublisher>();
        }
    }
}
