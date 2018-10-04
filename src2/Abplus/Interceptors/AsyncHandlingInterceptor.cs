using System.Reflection;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace Abp.Interceptors
{
    public abstract class AsyncHandlingInterceptor : IInterceptor
    {
        protected readonly IAsyncInterceptorHandler Handler;
        private static readonly MethodInfo HandleAsyncMethodInfo = typeof(AsyncHandlingInterceptor).GetMethod("HandleAsyncWithResult", BindingFlags.Instance | BindingFlags.NonPublic);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handler"></param>
        protected AsyncHandlingInterceptor(IAsyncInterceptorHandler handler)
        {
            Handler = handler;
        }

        public void Intercept(IInvocation invocation)
        {
            var delegateType = GetDelegateType(invocation);
            if (delegateType == MethodType.Synchronous)
            {
                Handler.Handle(invocation);
                //Handle(() => invocation.Proceed());
            }
            if (delegateType == MethodType.AsyncAction)
            {
                Handler.HandleAsync(invocation);
                //invocation.Proceed();
                //invocation.ReturnValue = HandleAsync((Task)invocation.ReturnValue);
            }
            if (delegateType == MethodType.AsyncFunction)
            {
                //invocation.Proceed();
                ExecuteHandleAsyncWithResultUsingReflection(invocation);
            }
        }
        private void ExecuteHandleAsyncWithResultUsingReflection(IInvocation invocation)
        {
            var resultType = invocation.Method.ReturnType.GetGenericArguments()[0];
            var mi = HandleAsyncMethodInfo.MakeGenericMethod(resultType);
            invocation.ReturnValue = mi.Invoke(this, new[] { invocation });
        }

        /// <summary>
        /// Used by Reflection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="invocation"></param>
        /// <returns></returns>
        protected virtual async Task<T> HandleAsyncWithResult<T>(IInvocation invocation)
        {
            return await Handler.HandleAsync<T>(invocation);
        }

        private MethodType GetDelegateType(IInvocation invocation)
        {
            var returnType = invocation.Method.ReturnType;
            if (returnType == typeof(Task))
                return MethodType.AsyncAction;
            if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(Task<>))
                return MethodType.AsyncFunction;
            return MethodType.Synchronous;
        }

        private enum MethodType
        {
            Synchronous,
            AsyncAction,
            AsyncFunction
        }
    }
}
