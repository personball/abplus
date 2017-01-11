using System;
using System.Threading.Tasks;
using Abp.MpIntegration.QrScan;
using Abp.Web.SignalR.Hubs;
using Castle.Core.Logging;
using Microsoft.AspNet.SignalR;

namespace Abp.Web.SignalR.QrScan
{
    public class SignalRQrScanNotifier : IQrScanNotifier
    {
        /// <summary>
        /// Reference to the logger.
        /// </summary>
        public ILogger Logger { get; set; }

        private static IHubContext CommonHub
        {
            get
            {
                return GlobalHost.ConnectionManager.GetHubContext<AbpCommonHub>();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SignalRRealTimeNotifier"/> class.
        /// </summary>
        public SignalRQrScanNotifier()
        {
            Logger = NullLogger.Instance;
        }

        public Task Notify(string scannerId, string connectionId, object properties)
        {
            try
            {
                var signalRClient = CommonHub.Clients.Client(connectionId);
                if (signalRClient == null)
                {
                    throw new Exception($"Can not find the client with connectionId:{connectionId}");
                }

                signalRClient.qrScanned(scannerId, properties);
            }
            catch (Exception ex)
            {
                Logger.Warn(ex.ToString(), ex);
            }

            return Task.FromResult(0);
        }
    }
}
