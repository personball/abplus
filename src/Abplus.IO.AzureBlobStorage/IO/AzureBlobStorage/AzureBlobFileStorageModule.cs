using System.Reflection;
using Abp.Modules;
using Abp.Localization;

namespace Abp.IO.AzureBlobStorage
{
    /// <summary>
    /// TODO 是否由本模块自行实现相关setting设置接口？
    /// </summary>
    [DependsOn(typeof(AbpKernelModule))]//依赖SettingManager
    public class AzureBlobFileStorageModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.Register<IFileStorage, AzureBlobFileStorage>();
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            AzureBlobStorageLocalizationConfigurer.Configure(Configuration.Localization);
        }
    }
}
