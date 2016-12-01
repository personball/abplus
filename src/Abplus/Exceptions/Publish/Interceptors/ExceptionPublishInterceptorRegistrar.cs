using Abp.Application.Services;
using Abp.Dependency;
using Castle.Core;
using Castle.MicroKernel;

namespace Abp.Exceptions.Publish.Interceptors
{
    public static class ExceptionPublishInterceptorRegistrar
    {
        public static void Initialize(IIocManager iocManager)
        {
            iocManager.Register<ExceptionPublishInterceptor>();
            iocManager.IocContainer.Kernel.ComponentRegistered += Kernel_ComponentRegistered;
        }

        private static void Kernel_ComponentRegistered(string key, IHandler handler)
        {
            if (typeof(IApplicationService).IsAssignableFrom(handler.ComponentModel.Implementation))
            {
                handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(ExceptionPublishInterceptor)));
            }
        }
    }
}
