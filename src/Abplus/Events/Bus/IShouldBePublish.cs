using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abp.Events.Bus
{
    /// <summary>
    ///this is used to mark an event should be publish.<see cref="PublishAllEventsHandler" />
    /// </summary>
    [Obsolete("不推荐直接将EventData发布到消息队列", false)]
    public interface IShouldBePublish
    {
    }
}
