using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace Abp.Interceptors
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAsyncInterceptorHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="invocation"></param>
        void Handle(IInvocation invocation);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="invocation"></param>
        /// <returns></returns>
        Task HandleAsync(IInvocation invocation);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="invocation"></param>
        /// <returns></returns>
        Task<T> HandleAsync<T>(IInvocation invocation);
    }
}
