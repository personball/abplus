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
    public interface IShouldBePublish
    {
    }
}
