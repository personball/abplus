namespace Abp.Extensions
{
    using System;

    /// <summary>
    /// Extension methods for <see cref="DateTime"/>.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 转换为完整时间的字符串(yyyy-MM-dd HH:mm:ss)
        /// </summary>
        public static string ToFullTimeString(this DateTime source)
        {
            return source.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 转换为短时间的字符串(yyyy-MM-dd HH:mm)
        /// </summary>
        public static string ToShortTimeString(this DateTime source)
        {
            return source.ToString("yyyy-MM-dd HH:mm");
        }

        /// <summary>
        /// 转换为只有日期的字符串(yyyy-MM-dd)
        /// </summary>
        public static string ToDateString(this DateTime source)
        {
            return source.ToString("yyyy-MM-dd");
        }
    }
}
