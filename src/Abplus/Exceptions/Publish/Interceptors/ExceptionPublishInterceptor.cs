using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Abp.Events.Bus;
using Abp.Extensions;
using Castle.Core.Logging;
using Castle.DynamicProxy;

namespace Abp.Exceptions.Publish.Interceptors
{
    public class ExceptionPublishInterceptor : IInterceptor
    {
        public ILogger Logger { get; set; }

        public void Intercept(IInvocation invocation)
        {
            var exceptionPublishAttributes = GetAttributesOfMemberAndDeclaringType<AbpExceptionPublishAttribute>(
                   invocation.MethodInvocationTarget);

            if (exceptionPublishAttributes.Count <= 0)
            {
                invocation.Proceed();
                return;
            }

            InterceptSync(invocation, exceptionPublishAttributes);
        }

        private void InterceptSync(IInvocation invocation, List<AbpExceptionPublishAttribute> exceptionPublishAttributes)
        {
            var exceptionTypes = exceptionPublishAttributes.SelectMany(attr => attr.ExceptionTypes).Distinct().ToList();

            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                if (exceptionTypes.Contains(ex.GetType()))
                {
                    EventBus.Default.Trigger(new AbpExceptionPublishEventData { Exception = ex.ToJsonString() });

                    Logger.Error(ex.ToJsonString());
                }

                //don't eat it!
                ex.ReThrow();
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
    }
}
