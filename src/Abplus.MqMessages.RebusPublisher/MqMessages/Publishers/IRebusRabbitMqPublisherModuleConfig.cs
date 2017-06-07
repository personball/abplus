using System;
using Rebus.Config;

namespace Abp.MqMessages.Publishers
{
    public interface IRebusRabbitMqPublisherModuleConfig
    {
        /// <summary>
        /// 是否启用，默认启用
        /// </summary>
        bool Enabled { get; }

        /// <summary>
        /// 日志配置委托
        /// </summary>
        Action<RebusLoggingConfigurer> LoggingConfigurer { get; }

        /// <summary>
        /// RabbitMq连接字符串
        /// </summary>
        string ConnectionString { get; }

        /// <summary>
        /// 消息审计是否开启，默认不开启
        /// </summary>
        bool MessageAuditingEnabled { get; }

        /// <summary>
        /// 消息审计队列名，默认不启用
        /// </summary>
        string MessageAuditingQueueName { get; }

        /// <summary>
        /// 是否启用，默认启用
        /// </summary>
        /// <param name="enabled"></param>
        /// <returns></returns>
        IRebusRabbitMqPublisherModuleConfig Enable(bool enabled);

        /// <summary>
        /// 设置RabbitMq连接字符串
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        IRebusRabbitMqPublisherModuleConfig ConnectionTo(string connectionString);

        /// <summary>
        /// 配置日志组件
        /// </summary>
        /// <param name="loggingConfigurer"></param>
        /// <returns></returns>
        IRebusRabbitMqPublisherModuleConfig UseLogging(Action<RebusLoggingConfigurer> loggingConfigurer);

        /// <summary>
        /// 是否启用消息审计，默认不启用
        /// </summary>
        /// <param name="messageAuditingQueueName">审计队列名</param>
        /// <returns></returns>
        IRebusRabbitMqPublisherModuleConfig EnableMessageAuditing(string messageAuditingQueueName);
    }
}
