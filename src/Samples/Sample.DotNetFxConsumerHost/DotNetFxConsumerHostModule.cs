using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.MqMessages.Consumers;
using Castle.Facilities.Logging;
using Castle.Services.Logging.NLogIntegration;
using Rebus.NLog.Config;
using System.Reflection;

namespace Sample.DotNetFxConsumerHost
{
    [DependsOn(typeof(RebusRabbitMqConsumerModule))]
    public class DotNetFxConsumerHostModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.UseAbplusRebusRabbitMqConsumer()
                .UseLogging(c => c.ColoredConsole())//c.NLog()
                .ConnectTo("amqp://dev:dev@rabbitmq.local.abplus.cn/")
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
