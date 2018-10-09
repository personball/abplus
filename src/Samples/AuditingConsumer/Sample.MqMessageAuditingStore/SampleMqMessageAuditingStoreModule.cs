using Abp.Auditing.AuditingStores;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.MqMessages.Publishers;
using Abp.Threading.BackgroundWorkers;
using Sample.MqMessageAuditingStore.BackgroudWorker;
using System.Reflection;

namespace Sample.MqMessageAuditingStore
{
    [DependsOn(
        typeof(RebusRabbitMqPublisherModule)//依赖消息发布机制
        , typeof(MqMessageAuditingStoreModule))]//加上模块依赖即可替换默认的审计日志存储机制
    public class SampleMqMessageAuditingStoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            //消息发布配置
            Configuration.Modules.UseAbplusRebusRabbitMqPublisher()
                .UseLogging(c => c.ColoredConsole())
                .ConnectTo("amqp://dev:dev@rabbitmq.local.abplus.cn/");//set your own connection string of rabbitmq

            Configuration.BackgroundJobs.IsJobExecutionEnabled = true;

            Configuration.Auditing.IsEnabled = true;
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;//开启匿名审计,TestAuditingStoreAppService没有要求用户认证
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        public override void PostInitialize()
        {
            //Abp.Dependency.IocManager.Instance.IocContainer.AddFacility<LoggingFacility>(f => f.LogUsing<NLogFactory>().WithConfig("nlog.config"));

            var workManager = IocManager.Resolve<IBackgroundWorkerManager>();
            workManager.Add(IocManager.Resolve<TestWorker>());//为了demo项目足够小,都用console中的worker进行演示,在mvc项目中使用ApplicationService效果相同
        }
    }
}