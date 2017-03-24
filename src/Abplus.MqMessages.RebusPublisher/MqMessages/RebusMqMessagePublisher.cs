using System.Threading.Tasks;
using Abp.Threading;
using Rebus.Bus;

namespace Abp.MqMessages
{
    public class RebusMqMessagePublisher : IMqMessagePublisher
    {
        private readonly IBus _bus;

        public RebusMqMessagePublisher(IBus bus)
        {
            _bus = bus;
        }

        public void Publish(object mqMessages)
        {
            AsyncHelper.RunSync(() => _bus.Publish(mqMessages));
        }

        public async Task PublishAsync(object mqMessages)
        {
            await _bus.Publish(mqMessages);
        }
    }
}
