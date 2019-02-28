using System.Reflection;
using Abp.IO.LocalFileSystem;
using Abp.Modules;

namespace Sample.LocalFileSystem
{
    [DependsOn(typeof(LocalFileSystemStorageModule))]
    internal class SampleLocalFileSystemModule : AbpModule
    {
        public override void PreInitialize()
        {
            
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        public override void PostInitialize()
        {
            //Abp.Dependency.IocManager.Instance.IocContainer.AddFacility<LoggingFacility>(f => f.LogUsing<NLogFactory>().WithConfig("nlog.config"));

            //var workManager = IocManager.Resolve<IBackgroundWorkerManager>();
            //workManager.Add(IocManager.Resolve<TestWorker>());
        }
    }
}