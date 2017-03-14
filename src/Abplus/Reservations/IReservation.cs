using System.Collections.Generic;
using Abp.TimeRanges;

namespace Abp.Reservations
{
    /// <summary>
    /// 预定
    /// </summary>
    public interface IReservation
    {
        /// <summary>
        /// 预定的主题
        /// </summary>
        string ReservationSubject { get; }

        /// <summary>
        /// 预定的类型
        /// </summary>
        string ReservationType { get; }

        /// <summary>
        /// 预定的唯一编码
        /// </summary>
        string ReservationCode { get; }

        /// <summary>
        /// 预定的时间
        /// </summary>
        TimeRange ReservationTime { get; }

        /// <summary>
        /// 同一预定主体的两个预定应检测是否冲突
        /// </summary>
        /// <param name="reservation"></param>
        /// <returns></returns>
        bool IsConflict(IReservation reservation);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reservations"></param>
        /// <returns></returns>
        bool IsConflict(IEnumerable<IReservation> reservations);
    }
}
