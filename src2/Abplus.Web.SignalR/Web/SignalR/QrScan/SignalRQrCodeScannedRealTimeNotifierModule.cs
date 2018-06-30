using Abp.Modules;
using Abp.QrCode;

namespace Abp.Web.SignalR.QrScan
{
    public class SignalRQrCodeScannedRealTimeNotifierModule : AbpModule
    {
        public override void PreInitialize()
        {
            //base.PreInitialize();
            IocManager.Register<IQrCodeScannedRealTimeNotifier, SignalRQrCodeScannedRealTimeNotifier>();
        }
    }
}
