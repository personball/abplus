using System.Reflection;
using Abp.Modules;
using Rebus.Bus;
using Rebus.CastleWindsor;
using Rebus.Config;
using Rebus.RabbitMq;
using Rebus.Auditing.Messages;

namespace Abp.Events.Producer.RebusRabbitMq
{
    [DependsOn(typeof(AbpEventsProducerModule))]
    public class AbpRebusRabbitMqProducerModule : AbpModule
    {
        private IBus _bus;
        public override void PreInitialize()
        {
            IocManager.Register<IAbpRebusRabbitMqProducerModuleConfig, AbpRebusRabbitMqProducerModuleConfig>();
            IocManager.Register<IProducer, RebusRabbitMqProducer>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        public override void PostInitialize()
        {
            var config = IocManager.Resolve<IAbpRebusRabbitMqProducerModuleConfig>();
            if (config.Enabled)
            {
                var container = IocManager.IocContainer;
                var adapter = new CastleWindsorContainerAdapter(container);

                var rebusConfig = Configure.With(adapter);
                if (config.MessageAuditingEnabled)
                {
                    rebusConfig.Options(o => o.EnableMessageAuditing(config.MessageAuditingQueueName));
                }

                rebusConfig.Logging(config.RebusLoggingConfig);

                _bus = rebusConfig.Transport(t => t.UseRabbitMqAsOneWayClient(config.ConnectionString))
                    .Start();
            }
        }
        public override void Shutdown()
        {
            if (_bus != null)
            {
                _bus.Dispose();
            }
        }
    }
}
