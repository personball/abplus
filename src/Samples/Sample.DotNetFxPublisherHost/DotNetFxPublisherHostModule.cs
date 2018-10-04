using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.MqMessages.Publishers;
using Abp.Threading.BackgroundWorkers;
using Castle.Facilities.Logging;
using Castle.Services.Logging.NLogIntegration;
using Rebus.NLog.Config;
using Sample.DotNetFxPublisherHost.BackgroundWorker;
using System.Reflection;

namespace Sample.DotNetFxPublisherHost
{
    [DependsOn(typeof(RebusRabbitMqPublisherModule))]
    public class DotNetFxPublisherHostModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.UseAbplusRebusRabbitMqPublisher()
                .UseLogging(c => c.ColoredConsole())//c.NLog()
                .ConnectTo("amqp://dev:dev@rabbitmq.local.abplus.cn/");//set your own connection string of rabbitmq

            Configuration.BackgroundJobs.IsJobExecutionEnabled = true;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        public override void PostInitialize()
        {
            Abp.Dependency.IocManager.Instance.IocContainer.AddFacility<LoggingFacility>(f => f.LogUsing<NLogFactory>().WithConfig("nlog.config"));

            var workManager = IocManager.Resolve<IBackgroundWorkerManager>();
            workManager.Add(IocManager.Resolve<TestWorker>());
        }
    }
}
