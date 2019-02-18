using System.Reflection;
using Abp.Localization;
using Abp.Modules;

namespace Abp.IO.LocalFileSystem
{
    //TODO 是否由本模块自行实现相关setting设置接口？
    [DependsOn(typeof(AbpKernelModule))]
    public class LocalFileSystemStorageModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.Register<ILocalFileSystemStorageConfig, LocalFileSystemStorageConfig>();
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            LocalFileSystemLocalizationConfigurer.Configure(Configuration.Localization);
        }
    }
}
