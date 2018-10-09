using System;
using System.Reflection;
using Rebus.Config;
using Rebus.Serialization;

namespace Abp.MqMessages.Consumers
{
    public interface IRebusRabbitMqConsumerModuleConfig
    {
        /// <summary>
        /// 是否启用，默认启用
        /// </summary>
        bool Enabled { get; }

        /// <summary>
        /// RabbitMq连接字符串
        /// </summary>
        string ConnectString { get; }

        /// <summary>
        /// 队列名
        /// </summary>
        string QueueName { get; }

        /// <summary>
        /// 最大并行数
        /// </summary>
        int MaxParallelism { get; }

        /// <summary>
        /// 最大Worker数
        /// </summary>
        int NumberOfWorkers { get; }

        /// <summary>
        /// 消费时每次拉取的消息个数
        /// </summary>
        int PrefetchCount { get; }

        /// <summary>
        /// 消息审计是否开启，默认不开启
        /// </summary>
        bool MessageAuditingEnabled { get; }

        /// <summary>
        /// 消息审计队列名，默认不启用
        /// </summary>
        string MessageAuditingQueueName { get; }

        /// <summary>
        /// 包含RebusMqMessageHandler的程序集(自动订阅消息和自动注册handler)
        /// </summary>
        Assembly[] AssemblysIncludeRebusMqMessageHandlers { get; }

        /// <summary>
        /// 配置日志组件
        /// </summary>
        Action<RebusLoggingConfigurer> LoggingConfigurer { get; }

        /// <summary>
        /// 其他选项配置
        /// </summary>
        Action<OptionsConfigurer> OptionsConfigurer { get; }

        /// <summary>
        /// 序列化组件配置
        /// </summary>
        Action<StandardConfigurer<ISerializer>> SerializerConfigurer { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enabled">是否启用，默认启用</param>
        /// <returns></returns>
        IRebusRabbitMqConsumerModuleConfig Enable(bool enabled);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectString">RabbitMq连接字符串</param>
        /// <returns></returns>
        IRebusRabbitMqConsumerModuleConfig ConnectTo(string connectString);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queueName">使用队列名</param>
        /// <returns></returns>
        IRebusRabbitMqConsumerModuleConfig UseQueue(string queueName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxParallelism">最大并行数</param>
        /// <returns></returns>
        IRebusRabbitMqConsumerModuleConfig SetMaxParallelism(int maxParallelism);

        /// <summary>
        /// 设置最大工作线程数
        /// </summary>
        /// <param name="numberOfWorkers">最大Worker数</param>
        /// <returns></returns>
        IRebusRabbitMqConsumerModuleConfig SetNumberOfWorkers(int numberOfWorkers);

        /// <summary>
        /// 设置每次拉取消息数量,默认50
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        IRebusRabbitMqConsumerModuleConfig Prefetch(int count);

        /// <summary>
        /// 启用消息审计
        /// </summary>
        /// <param name="messageAuditingQueueName">消息审计队列名</param>
        /// <returns></returns>
        IRebusRabbitMqConsumerModuleConfig EnableMessageAuditing(string messageAuditingQueueName);

        /// <summary>
        /// 注册Rebus Handlers
        /// </summary>
        /// <param name="assemblys">包含Rebus Handlers的程序集</param>
        /// <returns></returns>
        IRebusRabbitMqConsumerModuleConfig RegisterHandlerInAssemblys(params Assembly[] assemblys);

        /// <summary>
        /// 配置日志组件
        /// </summary>
        /// <param name="loggingConfigurer"></param>
        /// <returns></returns>
        IRebusRabbitMqConsumerModuleConfig UseLogging(Action<RebusLoggingConfigurer> loggingConfigurer);
        /// <summary>
        /// 配置其他选项
        /// </summary>
        /// <param name="optionsConfigurer"></param>
        /// <returns></returns>
        IRebusRabbitMqConsumerModuleConfig UseOptions(Action<OptionsConfigurer> optionsConfigurer);

        /// <summary>
        /// 自定义序列化机制
        /// </summary>
        /// <param name="serializerConfigurer"></param>
        /// <returns></returns>
        IRebusRabbitMqConsumerModuleConfig UseSerializer(Action<StandardConfigurer<ISerializer>> serializerConfigurer);
    }
}
