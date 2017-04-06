using System.Threading.Tasks;
using Abp.Json;
using Abp.Threading;
using Castle.Core.Logging;
using Rebus.Bus;

namespace Abp.MqMessages
{
    public class RebusMqMessagePublisher : IMqMessagePublisher
    {
        private readonly IBus _bus;

        public ILogger Logger { get; set; }

        public RebusMqMessagePublisher(IBus bus)
        {
            _bus = bus;
            Logger = NullLogger.Instance;
        }

        public void Publish(object mqMessages)
        {
            Logger.Debug(mqMessages.GetType().FullName + ":" + mqMessages.ToJsonString());

            AsyncHelper.RunSync(() => _bus.Publish(mqMessages));
        }

        public async Task PublishAsync(object mqMessages)
        {
            Logger.Debug(mqMessages.GetType().FullName + ":" + mqMessages.ToJsonString());

            await _bus.Publish(mqMessages);
        }
    }
}
