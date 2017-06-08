using System.Threading.Tasks;
using Castle.Core.Logging;
using Rebus.Handlers;
using Sample.MqMessages;

namespace Sample.Handlers
{
    public class TestHandler : IHandleMessages<TestMqMessage>
    {
        public ILogger Logger { get; set; }

        public TestHandler()
        {
            
        }

        public Task Handle(TestMqMessage message)
        {
            Logger.Debug($"{Logger.GetType()}:{message.Name},{message.Value},{message.Time}");

            return Task.FromResult(0);
        }
    }
}
