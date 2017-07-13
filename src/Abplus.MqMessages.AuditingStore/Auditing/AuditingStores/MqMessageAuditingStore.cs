using System;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.MqMessages;

namespace Abp.Auditing.AuditingStores
{
    public class MqMessageAuditingStore : IAuditingStore, ITransientDependency
    {
        private readonly Lazy<IMqMessagePublisher> MqMessagePublisher;//why this need Lazy<>?

        public MqMessageAuditingStore()
        {
            MqMessagePublisher = new Lazy<IMqMessagePublisher>(() =>
            {
                return IocManager.Instance.Resolve<IMqMessagePublisher>();
            });
        }

        public async Task SaveAsync(AuditInfo auditInfo)
        {
            var mqMsgAuditInfo = new AuditInfoMqMessage
            {
                BrowserInfo = auditInfo.BrowserInfo,
                ClientIpAddress = auditInfo.ClientIpAddress,
                ClientName = auditInfo.ClientName,
                CustomData = auditInfo.CustomData,
                ExecutionDuration = auditInfo.ExecutionDuration,
                ImpersonatorTenantId = auditInfo.ImpersonatorTenantId,
                ImpersonatorUserId = auditInfo.ImpersonatorUserId,
                MethodName = auditInfo.MethodName,
                Parameters = auditInfo.Parameters,
                ServiceName = auditInfo.ServiceName,
                UserId = auditInfo.UserId,
                ExecutionTime = auditInfo.ExecutionTime,
                TenantId = auditInfo.TenantId
            };

            if (auditInfo.Exception != null)
            {
                mqMsgAuditInfo.Exception = auditInfo.Exception.StackTrace;
                if (auditInfo.Exception.InnerException != null)
                {
                    mqMsgAuditInfo.Exception = auditInfo.Exception.InnerException.StackTrace;
                }
            }

            await MqMessagePublisher.Value.PublishAsync(mqMsgAuditInfo);
        }
    }
}
