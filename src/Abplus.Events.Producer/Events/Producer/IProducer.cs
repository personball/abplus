
using System;

namespace Abp.Events.Producer
{
    [Obsolete("不推荐直接将EventData发布到消息队列，请使用Abp.MqMessages.IMqMessagePublisher", false)]
    public interface IProducer
    {
        void Publish(object events);
    }
}
