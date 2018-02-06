﻿using System.Reflection;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.MqMessages.Publishers;
using Abp.Threading.BackgroundWorkers;
using Castle.Facilities.Logging;
using Rebus.NLog;
using Sample.BackgroundWorks;

namespace Sample
{
    [DependsOn(typeof(RebusRabbitMqPublisherModule))]
    public class SampleRebusRabbitMqPublisherModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.UseAbplusRebusRabbitMqPublisher()
                .UseLogging(c => c.NLog())
                .ConnectionTo("amqp://flymax:123456@localhost:5672");

            Configuration.BackgroundJobs.IsJobExecutionEnabled = true;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        public override void PostInitialize()
        {
            Abp.Dependency.IocManager.Instance.IocContainer.AddFacility<LoggingFacility>(f => f.UseNLog().WithConfig("nlog.config"));

            var workManager = IocManager.Resolve<IBackgroundWorkerManager>();
            workManager.Add(IocManager.Resolve<TestWorker>());
        }
    }
}
