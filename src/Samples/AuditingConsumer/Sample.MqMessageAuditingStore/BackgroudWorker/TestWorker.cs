using Abp.Dependency;
using Abp.Threading;
using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;
using Sample.MqMessageAuditingStore.Application;
using System;

namespace Sample.MqMessageAuditingStore.BackgroudWorker
{
    public class TestWorker : PeriodicBackgroundWorkerBase, ISingletonDependency
    {
        private readonly ITestAuditingStoreAppService _testApp;
        public TestWorker(AbpTimer timer, ITestAuditingStoreAppService testAuditingStoreAppService)
            : base(timer)
        {
            _testApp = testAuditingStoreAppService;
            Timer.Period = 1 * 1000;//1 seconds
            Timer.RunOnStart = true;
        }

        protected override void DoWork()
        {
            AsyncHelper.RunSync(() => _testApp.TestAuditing(new TestAuditingInput
            {
                Name = "Invoke Time",
                Value = DateTime.Now.ToLongTimeString()
            }));
        }
    }
}
