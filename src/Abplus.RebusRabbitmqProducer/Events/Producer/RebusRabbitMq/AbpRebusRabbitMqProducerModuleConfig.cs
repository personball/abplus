using System;
using Rebus.Config;

namespace Abp.Events.Producer.RebusRabbitMq
{
    internal class AbpRebusRabbitMqProducerModuleConfig : IAbpRebusRabbitMqProducerModuleConfig
    {
        public string ConnectionString { get; private set; }

        public bool Enabled { get; private set; }

        public bool MessageAuditingEnabled { get; private set; }
        public string MessageAuditingQueueName { get; private set; }

        public Action<RebusLoggingConfigurer> RebusLoggingConfig { get; private set; }

        public IAbpRebusRabbitMqProducerModuleConfig EnableMessageAuditing()
        {
            MessageAuditingEnabled = true;
            return this;
        }

        public IAbpRebusRabbitMqProducerModuleConfig SetMessageAuditingQueueName(string queueName)
        {
            MessageAuditingQueueName = queueName;
            return this;
        }

        public IAbpRebusRabbitMqProducerModuleConfig SetRebusLoggingConfigurer(Action<RebusLoggingConfigurer> loggingConfigurer)
        {
            RebusLoggingConfig = loggingConfigurer;
            return this;
        }

        public IAbpRebusRabbitMqProducerModuleConfig Enable()
        {
            Enabled = true;
            return this;
        }

        public IAbpRebusRabbitMqProducerModuleConfig Connect(string connectionString)
        {
            ConnectionString = connectionString;
            return this;
        }
    }
}
