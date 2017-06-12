using System;
using Abp.Logging;

namespace Abp.MqMessages.MqHandlers.ExceptionLogging
{
    public class ExceptionLogAttribute : Attribute
    {
        /// <summary>
        /// 需拦截的异常
        /// </summary>
        public Type[] ExceptionTypes { get; set; }

        /// <summary>
        /// 是否记录日志,默认true
        /// </summary>
        public bool Logged { get; set; }

        /// <summary>
        /// 日志级别，默认Warn
        /// </summary>
        public LogSeverity LogLevel { get; set; }

        /// <summary>
        /// 是否吃掉异常，默认false
        /// </summary>
        public bool NotThrow { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exceptionTypes"></param>
        public ExceptionLogAttribute(params Type[] exceptionTypes)
            : this(true, LogSeverity.Warn, false, exceptionTypes)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="notThrow"></param>
        /// <param name="exceptionTypes"></param>
        public ExceptionLogAttribute(bool notThrow, params Type[] exceptionTypes)
            : this(true, LogSeverity.Warn, notThrow, exceptionTypes)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logged"></param>
        /// <param name="logLevel"></param>
        /// <param name="exceptionTypes"></param>
        public ExceptionLogAttribute(bool logged, LogSeverity logLevel, params Type[] exceptionTypes)
            : this(logged, logLevel, false, exceptionTypes)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logged">记录日志（警告级别），默认true</param>
        /// <param name="logLevel">日志等级</param>
        /// <param name="notThrow">是否吃掉异常</param>
        /// <param name="exceptionTypes">需拦截的异常</param>
        public ExceptionLogAttribute(bool logged, LogSeverity logLevel, bool notThrow, params Type[] exceptionTypes)
        {
            Logged = logged;
            LogLevel = logLevel;
            NotThrow = notThrow;
            ExceptionTypes = exceptionTypes;
        }
    }
}
