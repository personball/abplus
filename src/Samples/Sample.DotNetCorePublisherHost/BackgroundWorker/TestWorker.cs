using Abp.Dependency;
using Abp.MqMessages;
using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;
using System;

namespace Sample.DotNetCorePublisherHost.BackgroundWorker
{
    public class TestWorker : PeriodicBackgroundWorkerBase, ISingletonDependency
    {
        private readonly IMqMessagePublisher _publisher;

        public TestWorker(AbpTimer timer, IMqMessagePublisher publisher)
            : base(timer)
        {
            _publisher = publisher;
            Timer.Period = 1 * 1000;//3 seconds
            Timer.RunOnStart = true;
        }

        protected override void DoWork()
        {
            Logger.Info($"TestWork Done! Time:{DateTime.Now}");
            _publisher.Publish(new TestMessage
            {
                Value = "TestWork from DotNetCoreHost:BlaBlaBlaBlaBlaBla",
                Time = DateTime.Now
            });
        }
    }
}
