using System;
using System.Reflection;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace Abp.Interceptors
{
    public abstract class AsyncHandlingInterceptor : IInterceptor
    {
        protected readonly IInterceptorHandler Handler;
        private static readonly MethodInfo HandleAsyncMethodInfo = typeof(AsyncHandlingInterceptor).GetMethod("HandleAsyncWithResult", BindingFlags.Instance | BindingFlags.NonPublic);

        protected AsyncHandlingInterceptor(IInterceptorHandler handler)
        {
            Handler = handler;
        }
        public void Intercept(IInvocation invocation)
        {
            var delegateType = GetDelegateType(invocation);
            if (delegateType == MethodType.Synchronous)
            {
                Handle(() => invocation.Proceed());
            }
            if (delegateType == MethodType.AsyncAction)
            {
                invocation.Proceed();
                invocation.ReturnValue = HandleAsync((Task)invocation.ReturnValue);
            }
            if (delegateType == MethodType.AsyncFunction)
            {
                invocation.Proceed();
                ExecuteHandleAsyncWithResultUsingReflection(invocation);
            }
        }
        private void ExecuteHandleAsyncWithResultUsingReflection(IInvocation invocation)
        {
            var resultType = invocation.Method.ReturnType.GetGenericArguments()[0];
            var mi = HandleAsyncMethodInfo.MakeGenericMethod(resultType);
            invocation.ReturnValue = mi.Invoke(this, new[] { invocation.ReturnValue });
        }

        protected virtual void Handle(Action invocation)
        {
            Handler.Handle(invocation);
        }

        protected virtual async Task HandleAsync(Task task)
        {
            await Handler.Handle(async () => await task);
        }

        protected virtual async Task<T> HandleAsyncWithResult<T>(Task<T> task)
        {
            return await Handler.Handle(async () => await task);
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
