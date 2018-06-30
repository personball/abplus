using Rebus.Bus;

namespace Abp.Events.Producer.RebusRabbitMq
{
    public class RebusRabbitMqProducer : IProducer
    {
        private IBus _bus;
        private IAbpRebusRabbitMqProducerModuleConfig _config;

        public RebusRabbitMqProducer(IBus bus, IAbpRebusRabbitMqProducerModuleConfig config)
        {
            _bus = bus;
            _config = config;
        }

        public void Publish(object events)
        {
            if (_config.Enabled)
            {
                _bus.Publish(events);
            }
        }
    }
}
