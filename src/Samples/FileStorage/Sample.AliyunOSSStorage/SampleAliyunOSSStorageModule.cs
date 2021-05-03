using System.Reflection;
using Abp.IO.AliyunOSSStorage;
using Abp.Modules;
using Abp.Configuration.Startup;
using Abp.Threading.BackgroundWorkers;
using Sample.AliyunOSSStorage.BackgroundWorker;

namespace Sample.AliyunOSSStorage
{
    [DependsOn(typeof(AliyunOSSStorageModule))]
    public class SampleAliyunOSSStorageModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.UseAliyunOSSStorage()
                .SetAccessKeyId("")
                .SetAccessKeySecret("")
                .SetBucketName("xxxxx")
                .SetEndpoint("oss-cn-hangzhou.aliyuncs.com")
                .WithUriPrefix("https://xxxxx.oss-cn-hangzhou.aliyuncs.com");
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