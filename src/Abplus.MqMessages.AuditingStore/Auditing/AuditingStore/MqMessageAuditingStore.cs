using System.Threading.Tasks;
using Abp.Dependency;
using Abp.MqMessages;

namespace Abp.Auditing.AuditingStore
{
    public class MqMessageAuditingStore : IAuditingStore, ITransientDependency
    {
        public IMqMessagePublisher MqMessagePublisher { get; set; }

        public MqMessageAuditingStore()
        {
            MqMessagePublisher = NullMqMessagePublisher.Instance;
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

            await MqMessagePublisher.PublishAsync(mqMsgAuditInfo);
        }
    }
}
