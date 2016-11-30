using System;
using System.Reflection;
using Abp.Modules;
using Rebus.Activation;
using Rebus.Auditing.Messages;
using Rebus.Config;
using Rebus.RabbitMq;

namespace Abp.Events.Consumer.RebusRabbitMq
{
    [DependsOn(typeof(AbpKernelModule))]
    public class AbpRebusRabbitMqConsumerModule : AbpModule
    {
        public Action<BuiltinHandlerActivator> RegisterHandlers;
        public Action<BuiltinHandlerActivator> SubscribeEvents;

        public Action<RebusConfigurer> RebusConfigAppend;

        private BuiltinHandlerActivator _activator;

        public AbpRebusRabbitMqConsumerModule()
        {
            _activator = new BuiltinHandlerActivator();
        }

        public override void PreInitialize()
        {
            IocManager.Register<IAbpRebusRabbitMqConsumerModuleConfig, AbpRebusRabbitMqConsumerModuleConfig>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        public override void PostInitialize()
        {
            var config = IocManager.Resolve<IAbpRebusRabbitMqConsumerModuleConfig>();

            RegisterHandlers(_activator);

            var rebusConfig = Configure.With(_activator);

            rebusConfig.Options(o => o.SetNumberOfWorkers(config.NumberOfWorkers));
            rebusConfig.Options(o => o.SetMaxParallelism(config.MaxParallelism));

            if (config.MessageAuditingEnabled)
            {
                rebusConfig.Options(o => o.EnableMessageAuditing(config.MessageAuditingQueueName));
            }

            if (config.RebusLoggingConfig != null)
            {
                rebusConfig.Logging(config.RebusLoggingConfig);
            }

            if (RebusConfigAppend != null)
            {
                RebusConfigAppend(rebusConfig);
            }

            rebusConfig.Transport(t => t.UseRabbitMq(config.ConnectionString, config.QueueName))
                .Start();

            SubscribeEvents(_activator);
        }

        public override void Shutdown()
        {
            //base.Shutdown();
            _activator.Bus.Dispose();
            _activator.Dispose();
        }

    }
}
