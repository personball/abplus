using Abp.Interceptors;

namespace Abp.MqMessages.MqHandlers.ExceptionLogging
{
    public class ExceptionLogInterceptor : AsyncHandlingInterceptor
    {
        public ExceptionLogInterceptor(ExceptionLogHandler handler) : base(handler)
        {
        }
    }
}
