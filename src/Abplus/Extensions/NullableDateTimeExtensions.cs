namespace Abp.Extensions
{
    using System;

    public static class NullableDateTimeExtensions
    {
        /// <summary>
        /// 转换为完整时间的字符串(yyyy-MM-dd HH:mm:ss)
        /// </summary>
        public static string ToFullString(this DateTime? source)
        {
            return source.HasValue ? source.Value.ToFullTimeString() : string.Empty;
        }

        /// <summary>
        /// 转换为短时间的字符串(yyyy-MM-dd HH:mm)
        /// </summary>
        public static string ToShortString(this DateTime? source)
        {
            return source.HasValue ? source.Value.ToShortTimeString() : string.Empty;
        }

        /// <summary>
        /// 转换为只有日期的字符串(yyyy-MM-dd)
        /// </summary>
        public static string ToDateString(this DateTime? source)
        {
            return source.HasValue ? source.Value.ToDateString() : string.Empty;
        }
    }
}
