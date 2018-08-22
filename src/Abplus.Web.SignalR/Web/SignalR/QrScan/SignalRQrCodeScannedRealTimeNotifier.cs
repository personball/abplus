using System;
using System.Threading.Tasks;
using Abp.AspNetCore.SignalR.Hubs;
using Abp.QrCode;
using Castle.Core.Logging;
using Microsoft.AspNetCore.SignalR;

namespace Abp.Web.SignalR.QrScan
{
    public class SignalRQrCodeScannedRealTimeNotifier : IQrCodeScannedRealTimeNotifier
    {
        /// <summary>
        /// Reference to the logger.
        /// </summary>
        public ILogger Logger { get; set; }

        private readonly IHubContext<AbpCommonHub> _commonHub;
       
        /// <summary>
        /// Initializes a new instance of the <see cref="SignalRRealTimeNotifier"/> class.
        /// </summary>
        public SignalRQrCodeScannedRealTimeNotifier(IHubContext<AbpCommonHub> commonHub)
        {
            Logger = NullLogger.Instance;
            _commonHub = commonHub;
        }

        public Task Notify(string scannerId, string connectionId, object properties = null)
        {
            try
            {
                var signalRClient = _commonHub.Clients.Client(connectionId);
                if (signalRClient == null)
                {
                    throw new Exception($"Can not find the client with connectionId:{connectionId}");
                }

                signalRClient.SendAsync("qrScanned",scannerId, properties);
            }
            catch (Exception ex)
            {
                Logger.Warn(ex.ToString(), ex);
            }

            return Task.FromResult(0);
        }
    }
}
