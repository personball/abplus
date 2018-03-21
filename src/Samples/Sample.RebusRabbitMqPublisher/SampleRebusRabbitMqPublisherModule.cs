using System.Reflection;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.MqMessages.Publishers;
using Abp.Threading.BackgroundWorkers;
using Castle.Facilities.Logging;
using Castle.Services.Logging.NLogIntegration;
using Sample.BackgroundWorks;

namespace Sample
{
    [DependsOn(typeof(RebusRabbitMqPublisherModule))]
    public class SampleRebusRabbitMqPublisherModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.UseAbplusRebusRabbitMqPublisher()
                //.UseLogging(c => c.NLog())
                .ConnectionTo("amqp://dev:dev@rabbitmq.local.jk724.cn/dev_host");

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
