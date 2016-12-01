using System;
using Abp.Events.Bus;

namespace Abp.Exceptions.Publish
{
    [Serializable]
    public class AbpExceptionPublishEventData : EventData, IShouldBePublish
    {
        public string Exception { get; set; }
    }
}
