using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.MqMessages.Consumers;
using System.Reflection;

namespace Sample.DotNetCoreConsumerHost
{
    [DependsOn(typeof(RebusRabbitMqConsumerModule))]
    public class SampleConsumerHostModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.UseAbplusRebusRabbitMqConsumer()
                .UseLogging(c => c.ColoredConsole())
                .ConnectTo("amqp://dev:dev@rabbitmq.local.abplus.cn/")//set your own connection string of rabbitmq
                .UseQueue(Assembly.GetExecutingAssembly().GetName().Name)
                .Prefetch(100)//用于控制每次拉取的资源消耗(内存,带宽),消费速度还要看消费端自己的消息处理速度
                .RegisterHandlerInAssemblys(Assembly.GetExecutingAssembly());
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void PostInitialize()
        {
            //Abp.Dependency.IocManager.Instance.IocContainer.AddFacility<LoggingFacility>(f => f.LogUsing<NLogFactory>().WithConfig("nlog.config"));
        }
    }
}