using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.MqMessages.Publishers;
using Abp.Threading.BackgroundWorkers;
using Sample.DotNetCorePublisherHost.BackgroundWorker;
using System.Reflection;

namespace Sample.DotNetCorePublisherHost
{
    [DependsOn(typeof(RebusRabbitMqPublisherModule))]
    public class SamplePublisherHostModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.UseAbplusRebusRabbitMqPublisher()
                .UseLogging(c => c.ColoredConsole())
                .ConnectTo("amqp://dev:dev@rabbitmq.local.abplus.cn/");

            Configuration.BackgroundJobs.IsJobExecutionEnabled = true;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        public override void PostInitialize()
        {
            //Abp.Dependency.IocManager.Instance.IocContainer.AddFacility<LoggingFacility>(f => f.LogUsing<NLogFactory>().WithConfig("nlog.config"));

            var workManager = IocManager.Resolve<IBackgroundWorkerManager>();
            workManager.Add(IocManager.Resolve<TestWorker>());
        }
    }
}
