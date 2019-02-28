using System.Reflection;
using Abp.Configuration.Startup;
using Abp.IO.AzureBlobStorage;
using Abp.Modules;
using Abp.Threading.BackgroundWorkers;
using Sample.AzureBlobStorage.BackgroundWorker;

namespace Sample.AzureBlobStorage
{
    [DependsOn(typeof(AzureBlobFileStorageModule))]
    public class SampleAzureBlobStorageModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.UseAzureBlobFileStorage()
                .ConfigAzureStorage()
                .SetAccountName("")
                .SetAccountKey("")
                .SetContainer("images")
                .UseEndpointSuffix("core.chinacloudapi.cn");

            //Configuration.Modules.UseAzureBlobFileStorage()
            //    .ConfigAzureStorageUseSettingManager();
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