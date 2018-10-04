using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Abp.Extensions;
using Abp.Interceptors;
using Castle.Core.Logging;
using Castle.DynamicProxy;

namespace Abp.MqMessages.MqHandlers.ExceptionLogging
{
    public class ExceptionLogHandler : IAsyncInterceptorHandler
    {
        public ILogger Logger { get; set; }

        public ExceptionLogHandler()
        {
            Logger = NullLogger.Instance;
        }

        public void Handle(IInvocation invocation)
        {
            var handleExceptionAttributes = GetAttributesOfMemberAndDeclaringType<ExceptionLogAttribute>(
                invocation.MethodInvocationTarget);

            if (handleExceptionAttributes.Count <= 0)
            {
                invocation.Proceed();
                return;
            }
            else
            {
                try
                {
                    invocation.Proceed();
                }
                catch (Exception ex)
                {
                    var exType = ex.GetType();
                    var attribute = handleExceptionAttributes.FirstOrDefault(h => h.ExceptionTypes.Contains(exType));

                    if (attribute == null)
                    {
                        ex.ReThrow();
                    }

                    LogException(attribute, ex, invocation.MethodInvocationTarget);

                    if (!attribute.NotThrow)
                    {
                        ex.ReThrow();
                    }
                }
            }
        }

        public async Task HandleAsync(IInvocation invocation)
        {
            var handleExceptionAttributes = GetAttributesOfMemberAndDeclaringType<ExceptionLogAttribute>(
                 invocation.MethodInvocationTarget);

            if (handleExceptionAttributes.Count <= 0)
            {
                invocation.Proceed();
                await (Task)invocation.ReturnValue;
            }
            else
            {
                try
                {
                    invocation.Proceed();
                    await (Task)invocation.ReturnValue;
                }
                catch (Exception ex)
                {
                    var exType = ex.GetType();
                    var attribute = handleExceptionAttributes.FirstOrDefault(h => h.ExceptionTypes.Contains(exType));

                    if (attribute == null)
                    {
                        ex.ReThrow();
                    }

                    LogException(attribute, ex, invocation.MethodInvocationTarget);

                    if (!attribute.NotThrow)
                    {
                        ex.ReThrow();
                    }
                }
            }
        }

        public async Task<T> HandleAsync<T>(IInvocation invocation)
        {
            var handleExceptionAttributes = GetAttributesOfMemberAndDeclaringType<ExceptionLogAttribute>(
               invocation.MethodInvocationTarget);

            if (handleExceptionAttributes.Count <= 0)
            {
                invocation.Proceed();
                return await (Task<T>)invocation.ReturnValue;
            }
            else
            {
                try
                {
                    invocation.Proceed();
                    return await (Task<T>)invocation.ReturnValue;
                }
                catch (Exception ex)
                {
                    var exType = ex.GetType();
                    var attribute = handleExceptionAttributes.FirstOrDefault(h => h.ExceptionTypes.Contains(exType));

                    if (attribute == null)
                    {
                        ex.ReThrow();
                    }

                    LogException(attribute, ex, invocation.MethodInvocationTarget);

                    if (!attribute.NotThrow)
                    {
                        ex.ReThrow();
                    }

                    return default(T);
                }
            }
        }

        private static List<TAttribute> GetAttributesOfMemberAndDeclaringType<TAttribute>(MemberInfo memberInfo)
           where TAttribute : Attribute
        {
            var attributeList = new List<TAttribute>();

            //Add attributes on the member
            if (memberInfo.IsDefined(typeof(TAttribute), true))
            {
                attributeList.AddRange(memberInfo.GetCustomAttributes(typeof(TAttribute), true).Cast<TAttribute>());
            }

            //Add attributes on the class
            if (memberInfo.DeclaringType != null && memberInfo.DeclaringType.IsDefined(typeof(TAttribute), true))
            {
                attributeList.AddRange(memberInfo.DeclaringType.GetCustomAttributes(typeof(TAttribute), true).Cast<TAttribute>());
            }

            return attributeList;
        }

        private void LogException(ExceptionLogAttribute attribute, Exception ex, MethodInfo methodInfo)
        {
            if (attribute.Logged)
            {
                var msg = $"{methodInfo.DeclaringType.FullName}.{$".{methodInfo.Name}"} Fail:{ex.Message}";

                switch (attribute.LogLevel)
                {
                    case Abp.Logging.LogSeverity.Debug:
                        Logger.Debug(msg, ex);
                        return;
                    case Abp.Logging.LogSeverity.Info:
                        Logger.Info(msg, ex);
                        return;
                    case Abp.Logging.LogSeverity.Warn:
                        Logger.Warn(msg, ex);
                        return;
                    case Abp.Logging.LogSeverity.Error:
                        Logger.Error(msg, ex);
                        return;
                    case Abp.Logging.LogSeverity.Fatal:
                        Logger.Fatal(msg, ex);
                        return;
                    default:
                        Logger.Warn(msg, ex);
                        return;
                }
            }
        }
    }
}
