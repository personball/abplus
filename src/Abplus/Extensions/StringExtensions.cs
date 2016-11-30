namespace Abp.Extensions
{
    using System;
    using Newtonsoft.Json;

    public static class StringExtensions
    {
        /// <summary>
        /// 字符串转换为Guid类型
        /// </summary>
        /// <param name="str"></param>
        /// <returns>默认返回Guid.Empty</returns>
        public static Guid ToGuid(this string str)
        {
            return str.ToGuid(null);
        }

        /// <summary>
        /// 指定格式字符串转换为Guid类型
        /// </summary>
        /// <param name="str"></param>
        /// <param name="format"></param>
        /// <returns>默认返回Guid.Empty</returns>
        public static Guid ToGuid(this string str, string format)
        {
            var result = Guid.Empty;
            if (!format.IsNullOrWhiteSpace())
            {
                Guid.TryParseExact(str, format, out result);
            }
            else
            {
                Guid.TryParse(str, out result);
            }

            return result;
        }

        /// <summary>
        /// 字符串转换为Guid类型或者null，不成功返回null
        /// </summary>
        public static Guid? ToGuidOrNull(this string str)
        {
            Guid? result = null;
            if (!string.IsNullOrWhiteSpace(str))
            {
                Guid tmp;
                if (Guid.TryParse(str, out tmp))
                {
                    result = tmp;
                }
            }

            return result;
        }

        /// <summary>
        /// 字符串转换为int类型或者null，不成功返回null
        /// </summary>
        public static int? ToIntOrNull(this string str)
        {
            int? result = null;
            if (!string.IsNullOrWhiteSpace(str))
            {
                int tmp;
                if (int.TryParse(str, out tmp))
                {
                    result = tmp;
                }
            }

            return result;
        }

        /// <summary>
        /// 将经过Base64编码的22位字符串还原为Guid
        /// </summary>
        public static Guid Base64ToGuid(this string str)
        {
            Guid result = Guid.Empty;
            str = str.Trim();
            string encoded = string.Concat(str.Trim().Replace("-", "+").Replace("_", "/"), "==");

            try
            {
                byte[] base64 = Convert.FromBase64String(encoded);
                result = new Guid(base64);
            }
            catch (Exception ex)
            {
                throw new AbpException("不是有效的参数格式", ex);
            }

            return result;
        }

        /// <summary>
        /// 由json字符串反序列化成指定类型的对象，字符串为空或不符合格式，则返回类型的默认值
        /// </summary>
        public static T ToObject<T>(this string json)
        {
            var obj = default(T);
            if (json.IsNullOrWhiteSpace())
            {
                return obj;
            }

            try
            {
                obj = JsonConvert.DeserializeObject<T>(json);
            }
            catch
            {
                //eat exception to return default value
            }

            return obj;
        }
    }
}
