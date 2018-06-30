using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
