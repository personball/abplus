
using System;
using Rebus.Config;
namespace Abp.Events.Consumer.RebusRabbitMq
{
    public class AbpRebusRabbitMqConsumerModuleConfig : IAbpRebusRabbitMqConsumerModuleConfig
    {
        public string ConnectionString { get; private set; }

        public string QueueName { get; private set; }

        public int NumberOfWorkers { get; private set; }
        public int MaxParallelism { get; private set; }

        public bool MessageAuditingEnabled { get; private set; }
        public string MessageAuditingQueueName { get; private set; }

        public Action<RebusLoggingConfigurer> RebusLoggingConfig { get; private set; }

        public AbpRebusRabbitMqConsumerModuleConfig()
        {
            NumberOfWorkers = 1;
            MaxParallelism = 1;
            MessageAuditingEnabled = false;
            MessageAuditingQueueName = "Audit";
        }

        public IAbpRebusRabbitMqConsumerModuleConfig Connect(string connectString)
        {
            ConnectionString = connectString;
            return this;
        }

        public IAbpRebusRabbitMqConsumerModuleConfig UseQueue(string queueName)
        {
            QueueName = queueName;
            return this;
        }

        public IAbpRebusRabbitMqConsumerModuleConfig SetNumberOfWorkers(int num)
        {
            NumberOfWorkers = num;
            return this;
        }
        public IAbpRebusRabbitMqConsumerModuleConfig SetMaxParallelism(int num)
        {
            MaxParallelism = num;
            return this;
        }

        public IAbpRebusRabbitMqConsumerModuleConfig EnableMessageAuditing()
        {
            MessageAuditingEnabled = true;
            return this;
        }

        public IAbpRebusRabbitMqConsumerModuleConfig SetMessageAuditingQueueName(string queueName)
        {
            MessageAuditingQueueName = queueName;
            return this;
        }

        public IAbpRebusRabbitMqConsumerModuleConfig SetRebusLoggingConfigurer(Action<RebusLoggingConfigurer> loggingConfigurer)
        {
            RebusLoggingConfig = loggingConfigurer;
            return this;
        }
    }
}
