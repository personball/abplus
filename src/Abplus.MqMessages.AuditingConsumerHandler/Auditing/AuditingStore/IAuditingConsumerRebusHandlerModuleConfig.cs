using Abp.MqMessages.AuditingStores;
using System;
using System.Collections.Generic;

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
        TimeSpan Period { get; }

        /// <summary>
        /// 批量存储的委托
        /// </summary>
        Action<IEnumerable<AuditInfoMqMessage>> BatchStoreAction { get; }

        /// <summary>
        /// 设置执行间隔(单位:秒)
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        IAuditingConsumerRebusHandlerModuleConfig EveryPeriodIn(TimeSpan period);
        
        /// <summary>
        /// 设置每批大小
        /// </summary>
        /// <param name="batchSize"></param>
        /// <returns></returns>
        IAuditingConsumerRebusHandlerModuleConfig Batch(int batchSize);

        /// <summary>
        /// 批量存储逻辑,如何处理消息
        /// </summary>
        /// <param name="storeAction"></param>
        /// <returns></returns>
        IAuditingConsumerRebusHandlerModuleConfig Do(Action<IEnumerable<AuditInfoMqMessage>> storeAction);
    }
}
