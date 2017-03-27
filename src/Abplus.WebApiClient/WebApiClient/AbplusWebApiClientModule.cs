using System.Reflection;
using Abp.Modules;

namespace Abp.WebApiClient
{
    public class AbplusWebApiClientModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
