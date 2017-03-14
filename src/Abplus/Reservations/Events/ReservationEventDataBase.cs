using Abp.Events.Bus;
using Abp.TimeRanges;

namespace Abp.Reservations.Events
{
    /// <summary>
    /// 预定发生变化的事件基类
    /// </summary>
    public abstract class ReservationEventDataBase : EventData
    {
        /// <summary>
        /// 预定记录唯一标识
        /// </summary>
        public string ReservationCode { get; set; }
        /// <summary>
        /// 预定主题
        /// </summary>
        public string ReservationSubject { get; set; }
        /// <summary>
        /// 预定时间区间
        /// </summary>
        public TimeRange ReservationTimeRange { get; set; }
    }
}
