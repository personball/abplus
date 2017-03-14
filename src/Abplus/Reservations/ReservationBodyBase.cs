using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Entities;
using Abp.Events.Bus;
using Abp.TimeRanges;
using Abp.Timing;

namespace Abp.Reservations
{
    /// <summary>
    /// 可被预定主体的抽象基类
    /// </summary>
    /// <typeparam name="TReservation"></typeparam>
    /// <typeparam name="TPrimary"></typeparam>
    public abstract class ReservationBodyBase<TReservation, TPrimary> : AggregateRoot<TPrimary>, IReservationBody<TReservation> where TReservation : IReservation
    {
        /// <summary>
        /// 可被预定主体的唯一标识
        /// </summary>
        public string ResourceCode { get; protected set; }

        /// <summary>
        /// 预定列表
        /// </summary>
        public virtual ICollection<TReservation> Reservations { get; protected set; }

        /// <summary>
        /// 获取预定取消时应触发的事件
        /// </summary>
        protected abstract Func<TReservation, IEventData> GetReservationCancelledEventData { get; }
        /// <summary>
        /// 检查新增预定是否与已有预定相冲突
        /// </summary>
        protected abstract Func<TReservation, IEnumerable<TReservation>, bool> GetIfTheseReservationsConflict { get; }
        /// <summary>
        /// 获取预定成功时应触发的事件
        /// </summary>
        protected abstract Func<TReservation, IEventData> GetReserveSuccessEventData { get; }
        /// <summary>
        /// 如果新增预定与已有预定相冲突时，应抛出的具体异常
        /// </summary>
        protected abstract Action ThrowIfTheseReservationsConflict { get; }

        /// <summary>
        /// 取消预定
        /// </summary>
        /// <param name="reservationCode"></param>
        /// <returns></returns>
        public virtual TReservation CancelReservation(string reservationCode)
        {
            Check.NotNullOrWhiteSpace(reservationCode, nameof(reservationCode));

            var reservation = Reservations.FirstOrDefault(r => r.ReservationCode == reservationCode);
            if (reservation == null)
            {
                return reservation;
            }

            Reservations.Remove(reservation);

            DomainEvents.Add(GetReservationCancelledEventData(reservation));

            return reservation;
        }

        /// <summary>
        /// 预定
        /// </summary>
        /// <param name="subject">预定主题</param>
        /// <param name="from">时间区间起</param>
        /// <param name="to">时间区间止</param>
        /// <returns></returns>
        public virtual TReservation Reserve(string subject, DateTime from, DateTime to)
        {
            Check.NotNullOrWhiteSpace(subject, nameof(subject));

            TReservation newReservation = (TReservation)Activator.CreateInstance(typeof(TReservation), subject, new TimeRange(from, to), ResourceCode);

            var reservations = Reservations.Where(r => r.ReservationTime.To > Clock.Now).ToList();
            if (reservations.Any())
            {
                if (GetIfTheseReservationsConflict(newReservation, reservations))
                {
                    ThrowIfTheseReservationsConflict();
                }
            }

            Reservations.Add(newReservation);

            DomainEvents.Add(GetReserveSuccessEventData(newReservation));

            return newReservation;
        }
    }
}
