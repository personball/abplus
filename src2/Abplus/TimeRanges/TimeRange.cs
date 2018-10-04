using System;
using System.Collections.Generic;
using Abp.Domain.Values;

namespace Abp.TimeRanges
{
    public class TimeRange : ValueObject<TimeRange>
    {
        protected TimeRange() { }

        public TimeRange(DateTime from, DateTime to)
        {
            Check.NotNull(from, nameof(from));
            Check.NotNull(to, nameof(to));

            if (from >= to)
            {
                throw new ArgumentException("时间区间的起点必须小于终点！");
            }

            From = from;
            To = to;
        }

        /// <summary>
        /// 时间区间起
        /// </summary>
        public DateTime From { get; private set; }
        /// <summary>
        /// 时间区间止
        /// </summary>
        public DateTime To { get; private set; }

        /// <summary>
        /// 包含，一般用于匹配
        /// </summary>
        /// <param name="that"></param>
        /// <returns></returns>
        public bool IsIncluding(TimeRange that)
        {
            Check.NotNull(that, nameof(that));

            return From <= that.From && To >= that.To;
        }

        /// <summary>
        /// 相交，一般用于检测冲突
        /// </summary>
        /// <param name="that"></param>
        /// <returns></returns>
        public bool IsIntersect(TimeRange that)
        {
            Check.NotNull(that, nameof(that));

            return To >= that.From && To <= that.To
                || From >= that.From && To <= that.To
                || From >= that.From && From <= that.To;
        }

        /// <summary>
        /// 相交，一般用于检测冲突
        /// </summary>
        /// <param name="those"></param>
        /// <returns></returns>
        public bool IsIntersect(ICollection<TimeRange> those)
        {
            Check.NotNull(those, nameof(those));
            foreach (var that in those)
            {
                if (IsIntersect(that))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
