namespace Abp.Extensions
{
    using System;

    public static class GuidExtensions
    {
        /// <summary>
        /// 将Guid转换为经过Base64编码的22位字符串
        /// </summary>
        public static string ToShortString(this Guid source)
        {
            string base64 = Convert.ToBase64String(source.ToByteArray());
            string result = base64.Replace("/", "_").Replace("+", "-").Substring(0, 22);
            return result;
        }

        /// <summary>
        /// 转换成sqlserver有序Guid,时间相关，精度为300分之一毫秒
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Guid ToCombGuid(this Guid source)
        {
            byte[] guidArray = source.ToByteArray();
            DateTime baseDate = new DateTime(1900, 1, 1);
            DateTime now = DateTime.Now;
            // Get the days and milliseconds which will be used to build the byte string 
            TimeSpan days = new TimeSpan(now.Ticks - baseDate.Ticks);
            TimeSpan msecs = new TimeSpan(now.Ticks - (new DateTime(now.Year, now.Month, now.Day).Ticks));
            // Convert to a byte array 
            // Note that SQL Server is accurate to 1/300th of a millisecond so we divide by 3.333333 
            byte[] daysArray = BitConverter.GetBytes(days.Days);
            byte[] msecsArray = BitConverter.GetBytes((long)(msecs.TotalMilliseconds / 3.333333));
            // Reverse the bytes to match SQL Servers ordering 
            Array.Reverse(daysArray);
            Array.Reverse(msecsArray);
            // Copy the bytes into the guid 
            Array.Copy(daysArray, daysArray.Length - 2, guidArray, guidArray.Length - 6, 2);
            Array.Copy(msecsArray, msecsArray.Length - 4, guidArray, guidArray.Length - 4, 4);
            return new Guid(guidArray);
        }
    }
}
