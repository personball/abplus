using System.Reflection;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.MqMessages.Consumers;
using Castle.Facilities.Logging;
using Castle.Services.Logging.NLogIntegration;
using Rebus.NLog.Config;

namespace Sample
{
    [DependsOn(typeof(RebusRabbitMqConsumerModule))]
    public class SampleRebusRabbitMqConsumerModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.UseAbplusRebusRabbitMqConsumer()
                .UseLogging(c => c.NLog())
                .ConnectTo("amqp://dev:dev@rabbitmq.local.jk724.cn/dev_host")
                .UseQueue(Assembly.GetExecutingAssembly().GetName().Name)
                .RegisterHandlerInAssemblys(Assembly.GetExecutingAssembly());
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void PostInitialize()
        {
            Abp.Dependency.IocManager.Instance.IocContainer.AddFacility<LoggingFacility>(f => f.LogUsing<NLogFactory>().WithConfig("nlog.config"));
        }
    }
}
