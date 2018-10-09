using Abp.Application.Services;
using System.Threading.Tasks;

namespace Sample.MqMessageAuditingStore.Application
{
    public interface ITestAuditingStoreAppService : IApplicationService
    {
        Task TestAuditing(TestAuditingInput input);
    }
}
