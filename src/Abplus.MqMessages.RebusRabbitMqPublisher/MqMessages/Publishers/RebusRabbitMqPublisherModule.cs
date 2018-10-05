using System.Reflection;
using Abp.Modules;
using Rebus.Auditing.Messages;
using Rebus.Bus;
using Rebus.Config;

namespace Abp.MqMessages.Publishers
{
    [DependsOn(typeof(RebusRabbitMqPublisherCoreModule))]
    public class RebusRabbitMqPublisherModule : AbpModule
    {
        private IBus _bus;

        public override void PreInitialize()
        {
            IocManager.Register<IRebusRabbitMqPublisherModuleConfig, RebusRabbitMqPublisherModuleConfig>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        public override void PostInitialize()
        {
            var moduleConfig = IocManager.Resolve<IRebusRabbitMqPublisherModuleConfig>();
            if (moduleConfig.Enabled)
            {
                var rebusConfig = Configure.With(new CastleWindsorContainerAdapter(IocManager.IocContainer));
                if (moduleConfig.MessageAuditingEnabled)
                {
                    rebusConfig.Options(o => o.EnableMessageAuditing(moduleConfig.MessageAuditingQueueName));
                }

                if (moduleConfig.LoggingConfigurer != null)
                {
                    rebusConfig.Logging(moduleConfig.LoggingConfigurer);
                }

                //https://github.com/rebus-org/Rebus.RabbitMq/tree/master/Rebus.RabbitMq/Config  more option about RabbitMqOptionsBuilder
                _bus = rebusConfig.Transport(t => t.UseRabbitMqAsOneWayClient(moduleConfig.ConnectionString))
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
