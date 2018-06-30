
using System;
using Rebus.Config;
namespace Abp.Events.Consumer.RebusRabbitMq
{
    public interface IAbpRebusRabbitMqConsumerModuleConfig
    {
        string ConnectionString { get; }
        string QueueName { get; }
        int NumberOfWorkers { get; }
        int MaxParallelism { get; }

        bool MessageAuditingEnabled { get; }
        string MessageAuditingQueueName { get; }
        Action<RebusLoggingConfigurer> RebusLoggingConfig { get; }

        IAbpRebusRabbitMqConsumerModuleConfig Connect(string connectString);
        IAbpRebusRabbitMqConsumerModuleConfig UseQueue(string queueName);
        IAbpRebusRabbitMqConsumerModuleConfig SetNumberOfWorkers(int num);
        IAbpRebusRabbitMqConsumerModuleConfig SetMaxParallelism(int num);
        IAbpRebusRabbitMqConsumerModuleConfig EnableMessageAuditing();
        IAbpRebusRabbitMqConsumerModuleConfig SetMessageAuditingQueueName(string queueName);

        IAbpRebusRabbitMqConsumerModuleConfig SetRebusLoggingConfigurer(Action<RebusLoggingConfigurer> loggingConfigurer);
    }
}
