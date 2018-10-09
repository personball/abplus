using System;
using System.Threading.Tasks;
using Abp.Json;

namespace Sample.MqMessageAuditingStore.Application
{
    public class TestAuditingStoreAppService : ITestAuditingStoreAppService
    {
        public Task TestAuditing(TestAuditingInput input)
        {
            Console.WriteLine(input.ToJsonString());
            return Task.FromResult(0);
        }
    }
}
