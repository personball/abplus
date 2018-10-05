using Abp.MqMessages.AuditingStores;
using System;
using System.Collections.Generic;

namespace Abp.Auditing.AuditingStore
{
    public class AuditingConsumerRebusHandlerModuleConfig : IAuditingConsumerRebusHandlerModuleConfig
    {
        public AuditingConsumerRebusHandlerModuleConfig()
        {
            BatchSize = 100;
            PeriodInSeconds = 1;
            BatchStoreAction = (msgList) => { };
        }
        public int BatchSize { get; private set; }

        public Action<IEnumerable<AuditInfoMqMessage>> BatchStoreAction { get; private set; }

        public int PeriodInSeconds { get; private set; }

        public IAuditingConsumerRebusHandlerModuleConfig Batch(int batchSize)
        {
            BatchSize = batchSize;
            return this;
        }

        public IAuditingConsumerRebusHandlerModuleConfig Do(Action<IEnumerable<AuditInfoMqMessage>> storeAction)
        {
            BatchStoreAction = storeAction;
            return this;
        }

        public IAuditingConsumerRebusHandlerModuleConfig EveryPeriodInSeconds(int seconds)
        {
            PeriodInSeconds = seconds;
            return this;
        }
    }
}
