using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Abp.Json
{
    /// <summary>
    /// csharp long type number maybe overflow when it assigned to javascript in a json object, so serialize it as string when its value overflow.
    /// </summary>
    public class LargeNumJsonConverter : JsonConverter
    {

        public override bool CanRead => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var num = value as long?;
            if (num.HasValue && (num > _maxJsNum || num < _minJsNum))
            {
                writer.WriteValue(num.ToString());
            }
            else
            {
                writer.WriteValue(value);
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (LargeNumTypes.Contains(objectType))
            {
                long result;
                if (reader.Value != null && long.TryParse(reader.Value.ToString(), out result))
                {
                    return result;
                }
                return (long?)null;
            }
            return reader.Value;
        }

        public override bool CanConvert(Type objectType)
        {
            //9007199254740992
            return LargeNumTypes.Contains(objectType);
        }

        private static long _maxJsNum = 9007199254740992;
        private static long _minJsNum = -9007199254740992;

        private static readonly List<Type> LargeNumTypes = new List<Type>
            {
                typeof(long),
                typeof(long?)
            };
    }
}
