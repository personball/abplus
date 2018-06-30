using System;
using System.Collections.Generic;

namespace Abp.Reservations
{
    /// <summary>
    /// 可被预定的资源主体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IReservationBody<T> where T : IReservation
    {
        /// <summary>
        /// 可被预定的资源唯一标识（建议8位数字）
        /// </summary>
        string ResourceCode { get; }

        /// <summary>
        /// 资源的预定列表
        /// </summary>
        ICollection<T> Reservations { get; }

        /// <summary>
        /// 预定
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        T Reserve(string subject, DateTime from, DateTime to);

        /// <summary>
        /// 取消预定
        /// </summary>
        /// <param name="reservationCode"></param>
        /// <returns></returns>
        T CancelReservation(string reservationCode);
    }
}
