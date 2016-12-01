using System;

namespace Abp.Exceptions.Publish
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AbpExceptionPublishAttribute : Attribute
    {
        public Type[] ExceptionTypes { get; private set; }

        public AbpExceptionPublishAttribute(params Type[] exceptionTypes)
        {
            ExceptionTypes = exceptionTypes;
        }
    }
}
