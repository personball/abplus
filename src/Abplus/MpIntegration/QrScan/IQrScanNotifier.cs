using System.Threading.Tasks;

namespace Abp.MpIntegration.QrScan
{
    public interface IQrScanNotifier
    {
        Task Notify(string scannerId, string connectionId, object properties = null);
    }
}
