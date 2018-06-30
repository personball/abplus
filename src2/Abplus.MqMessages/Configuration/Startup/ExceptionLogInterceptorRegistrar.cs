using Abp.Dependency;
using Abp.MqMessages.MqHandlers;
using Abp.MqMessages.MqHandlers.ExceptionLogging;
using Castle.Core;
using Castle.MicroKernel;

namespace Abp.Configuration.Startup
{
    public static class ExceptionLogInterceptorRegistrar
    {
        public static void Initialize(IIocManager iocManager)
        {
            iocManager.Register<ExceptionLogHandler>();
            iocManager.Register<ExceptionLogInterceptor>();
            iocManager.IocContainer.Kernel.ComponentRegistered += Kernel_ComponentRegistered;
        }

        private static void Kernel_ComponentRegistered(string key, IHandler handler)
        {
            if (typeof(AbpMqHandlerBase).IsAssignableFrom(handler.ComponentModel.Implementation))
            {
                handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(ExceptionLogInterceptor)));
            }
        }
    }
}
