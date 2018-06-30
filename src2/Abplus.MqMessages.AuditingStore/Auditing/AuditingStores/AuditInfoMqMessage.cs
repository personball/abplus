using System;

namespace Abp.Auditing.AuditingStores
{
    public class AuditInfoMqMessage
    {
        /// <summary>
        /// TenantId.
        /// </summary>
        public int? TenantId { get; set; }

        /// <summary>
        /// UserId.
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// ImpersonatorUserId.
        /// </summary>
        public long? ImpersonatorUserId { get; set; }

        /// <summary>
        /// ImpersonatorTenantId.
        /// </summary>
        public int? ImpersonatorTenantId { get; set; }

        /// <summary>
        /// Service (class/interface) name.
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// Executed method name.
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// Calling parameters.
        /// </summary>
        public string Parameters { get; set; }

        /// <summary>
        /// Start time of the method execution.
        /// </summary>
        public DateTime ExecutionTime { get; set; }

        /// <summary>
        /// Total duration of the method call.
        /// </summary>
        public int ExecutionDuration { get; set; }

        /// <summary>
        /// IP address of the client.
        /// </summary>
        public string ClientIpAddress { get; set; }

        /// <summary>
        /// Name (generally computer name) of the client.
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// Browser information if this method is called in a web request.
        /// </summary>
        public string BrowserInfo { get; set; }

        /// <summary>
        /// Optional custom data that can be filled and used.
        /// </summary>
        public string CustomData { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Exception { get; set; }
    }
}
