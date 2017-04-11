using System.Threading.Tasks;

namespace Abp.QrCode
{
    public interface IQrCodeScannedRealTimeNotifier
    {
        Task Notify(string scannerIdentifier, string connectionId, object properties = null);
    }
}
