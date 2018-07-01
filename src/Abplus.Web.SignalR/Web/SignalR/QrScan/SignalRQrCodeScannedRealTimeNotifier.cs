using System;
using System.Threading.Tasks;
using Abp.QrCode;
using Abp.Web.SignalR.Hubs;
using Castle.Core.Logging;
using Microsoft.AspNet.SignalR;

namespace Abp.Web.SignalR.QrScan
{
    public class SignalRQrCodeScannedRealTimeNotifier : IQrCodeScannedRealTimeNotifier
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
        public SignalRQrCodeScannedRealTimeNotifier()
        {
            Logger = NullLogger.Instance;
        }

        public Task Notify(string scannerId, string connectionId, object properties = null)
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
