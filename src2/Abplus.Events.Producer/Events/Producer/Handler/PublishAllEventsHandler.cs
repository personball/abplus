using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Events.Bus;
using Abp.Events.Bus.Handlers;

namespace Abp.Events.Producer.Handler
{
    public class PublishAllEventsHandler : IEventHandler<EventData>, ITransientDependency
    {
        public IProducer _producer { get; set; }
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public PublishAllEventsHandler(IUnitOfWorkManager unitOfWorkManager)
        {
            _producer = NullProducer.Instance;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public void HandleEvent(EventData eventData)
        {
            if (eventData is IShouldBePublish)
            {
                if (_unitOfWorkManager.Current == null)
                {
                    _producer.Publish(eventData);
                }
                else
                {
                    _unitOfWorkManager.Current.Completed += (sender, e) => _producer.Publish(eventData);
                }
            }
        }

    }
}
