using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Events.Bus;
using Abp.Events.Bus.Handlers;
using Castle.Core.Logging;

namespace Abp.MqMessages.Handlers
{
    /// <summary>
    /// 订阅EventData并发布消息到消息队列的抽象基类
    /// </summary>
    /// <typeparam name="TEventData">Abp事件</typeparam>
    /// <typeparam name="TMqMessage">支持序列化的消息体（类DTO对象）</typeparam>
    public abstract class EventDataPublishHandlerBase<TEventData, TMqMessage>
        : IEventHandler<TEventData>, ITransientDependency
        where TEventData : EventData
        where TMqMessage : class
    {
        protected readonly IUnitOfWorkManager UnitOfWorkManager;

        public ILogger Logger { get; set; }

        public IMqMessagePublisher MqMessagePublisher { get; set; }

        public EventDataPublishHandlerBase(IUnitOfWorkManager unitOfWorkManager)
        {
            UnitOfWorkManager = unitOfWorkManager;
            Logger = NullLogger.Instance;
            MqMessagePublisher = NullMqMessagePublisher.Instance;
        }

        public virtual void HandleEvent(TEventData eventData)
        {
            if (UnitOfWorkManager.Current == null)
            {
                MqMessagePublisher.Publish(eventData.MapTo<TMqMessage>());
            }
            else
            {
                UnitOfWorkManager.Current.Completed += (sender, e) => MqMessagePublisher.Publish(eventData.MapTo<TMqMessage>());
            }
        }
    }
}
