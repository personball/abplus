using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Auditing.AuditingStores;

namespace Abp.Auditing.AuditingStore
{
    public interface IAuditingConsumerRebusHandlerModuleConfig
    {
        /// <summary>
        /// 每批大小
        /// </summary>
        int BatchSize { get; }

        /// <summary>
        /// 执行间隔（单位：秒）
        /// </summary>
        int PeriodInSeconds { get; }

        /// <summary>
        /// 批量存储的委托
        /// </summary>
        Action<IEnumerable<AuditInfoMqMessage>> BatchStoreAction { get; }
    }
}
