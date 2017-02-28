using Abp.Web.SimpleCaptcha;

namespace Abp.Configuration.Startup
{
    public static class SimpleCaptchaManagerConfigurationExtension
    {
        public static ISimpleCaptchaModuleConfig UseSimpleCaptchaModule(this IModuleConfigurations configurations)
        {
            return configurations.AbpConfiguration.GetOrCreate("Modules.Abplus.Web.SimpleCaptcha.ISimpleCaptchaModuleConfig",
                () => configurations.AbpConfiguration.IocManager.Resolve<ISimpleCaptchaModuleConfig>());
        }
    }
}
