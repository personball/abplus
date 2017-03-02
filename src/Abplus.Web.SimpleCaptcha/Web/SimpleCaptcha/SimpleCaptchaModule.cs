using System.Reflection;
using Abp.Dependency;
using Abp.Modules;
using Abp.Web.SimpleCaptcha.VerificationCodeStores;

namespace Abp.Web.SimpleCaptcha
{
    public class SimpleCaptchaModule : AbpModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<ISimpleCaptchaModuleConfig, SimpleCaptchaModuleConfig>();
        }

        public override void Initialize()
        {
            IocManager.RegisterIfNot<IVerificationCodeStore, SessionVerificationCodeStore>();
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
