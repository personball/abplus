using Abp.Dependency;
using Abp.MqMessages.AuditingStores;
using Abp.Threading.Timers;
using Rebus.Handlers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abp.Auditing.AuditingStore
{
    public class MqMessageAuditingStoreRebusHandler :
         IHandleMessages<AuditInfoMqMessage>
        , ITransientDependency
    {
        private static ConcurrentQueue<AuditInfoMqMessage> InMemoryAuditInfoQueue;
        public static AbpTimer Timer;

        static MqMessageAuditingStoreRebusHandler()
        {
            InMemoryAuditInfoQueue = new ConcurrentQueue<AuditInfoMqMessage>();

            var config = IocManager.Instance.Resolve<IAuditingConsumerRebusHandlerModuleConfig>();
            Timer = new AbpTimer(config.PeriodInSeconds * 1000);

            Timer.Elapsed += (s, e) =>
            {
                var tmpMsg = new List<AuditInfoMqMessage>();
                for (int i = 0; i < config.BatchSize; i++)
                {
                    AuditInfoMqMessage msg;
                    if (InMemoryAuditInfoQueue.TryDequeue(out msg))
                    {
                        tmpMsg.Add(msg);
                    }
                    else
                    {
                        break;
                    }
                }

                if (tmpMsg.Any())
                {
                    config.BatchStoreAction(tmpMsg);
                }
            };
        }

        public async Task Handle(AuditInfoMqMessage message)
        {
            InMemoryAuditInfoQueue.Enqueue(message);
        }
    }
}
