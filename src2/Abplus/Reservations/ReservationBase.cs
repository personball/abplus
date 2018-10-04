using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Abp.Domain.Entities.Auditing;
using Abp.TimeRanges;

namespace Abp.Reservations
{
    /// <summary>
    /// 预定基类，可被预定的资源拥有一个预定的列表，具体预定的实现可继承本基类
    /// </summary>
    public abstract class ReservationBase : CreationAuditedEntity<Guid>, IReservation
    {
        /// <summary>
        /// 默认最小预定时间区间长度（单位：分钟）
        /// </summary>
        public const int DefaultMinTimeRangeLengthForReservationInMinutes = 3;//预定记录唯一编码受预定时间区间粒度影响

        protected ReservationBase() { }

        public ReservationBase(string reservationSubject, TimeRange reservationTime, string reservationType, string reservationResourceCode)
        {
            if (reservationTime.To.Subtract(reservationTime.From).TotalMinutes < MinTimeRangeLengthForReservationInMinutes)
            {
                throw new ArgumentException($"预定时间区间应大于{MinTimeRangeLengthForReservationInMinutes}分钟!");
            }

            ReservationType = reservationType;
            ReservationSubject = reservationSubject;
            ReservationTime = reservationTime;

            SetReservationCode(reservationResourceCode);
        }

        protected virtual int MinTimeRangeLengthForReservationInMinutes
        {
            get
            {
                return DefaultMinTimeRangeLengthForReservationInMinutes;
            }
        }

        /// <summary>
        /// 生成预定唯一编码
        /// </summary>
        /// <param name="resourceCode"></param>
        protected virtual void SetReservationCode(string resourceCode)
        {
            ReservationCode = $"{ReservationType}-{resourceCode}-{ReservationTime.From.ToString("yyyyMMddHHmm")}";
        }

        /// <summary>
        /// 预定类型
        /// </summary>
        [MaxLength(10)]
        public string ReservationType { get; private set; }

        /// <summary>
        /// 预定唯一编码
        /// </summary>
        public string ReservationCode { get; private set; }

        /// <summary>
        /// 预定主题
        /// </summary>
        [MaxLength(256)]
        public string ReservationSubject { get; private set; }

        /// <summary>
        /// 所预定的时间区间
        /// </summary>
        public TimeRange ReservationTime { get; private set; }

        /// <summary>
        /// 是否冲突
        /// </summary>
        /// <param name="reservation"></param>
        /// <returns></returns>
        public virtual bool IsConflict(IReservation reservation)
        {
            return ReservationTime.IsIntersect(reservation.ReservationTime);
        }

        /// <summary>
        /// 是否冲突
        /// </summary>
        /// <param name="reservations"></param>
        /// <returns></returns>
        public virtual bool IsConflict(IEnumerable<IReservation> reservations)//ICollection<IReservation> 不支持逆变
        {
            var times = reservations.Select(r => r.ReservationTime).ToList();
            return ReservationTime.IsIntersect(times);
        }
    }
}
