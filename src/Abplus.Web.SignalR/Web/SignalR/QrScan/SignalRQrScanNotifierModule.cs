using Abp.Modules;
using Abp.MpIntegration.QrScan;

namespace Abp.Web.SignalR.QrScan
{
    public class SignalRQrScanNotifierModule : AbpModule
    {
        public override void PreInitialize()
        {
            //base.PreInitialize();
            IocManager.Register<IQrScanNotifier, SignalRQrScanNotifier>();
        }
    }
}
