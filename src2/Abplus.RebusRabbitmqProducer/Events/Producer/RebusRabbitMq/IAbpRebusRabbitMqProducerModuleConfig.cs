using System;
using Rebus.Config;

namespace Abp.Events.Producer.RebusRabbitMq
{
    public interface IAbpRebusRabbitMqProducerModuleConfig
    {
        string ConnectionString { get; }
        bool Enabled { get; }
        bool MessageAuditingEnabled { get; }
        string MessageAuditingQueueName { get; }
        Action<RebusLoggingConfigurer> RebusLoggingConfig { get; }
        IAbpRebusRabbitMqProducerModuleConfig EnableMessageAuditing();
        IAbpRebusRabbitMqProducerModuleConfig SetMessageAuditingQueueName(string queueName);

        IAbpRebusRabbitMqProducerModuleConfig SetRebusLoggingConfigurer(Action<RebusLoggingConfigurer> loggingConfigurer);
        IAbpRebusRabbitMqProducerModuleConfig Enable();
        IAbpRebusRabbitMqProducerModuleConfig Connect(string connectionString);
    }
}
