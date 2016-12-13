using System.Net.Http.Formatting;
using Abp.Json;

namespace Abp.WebApi
{
    public static class JsonMediaTypeFormatterExtension
    {
        public static void SetCustomDataFormatString(this JsonMediaTypeFormatter formatter, string dateTimeFormat)
        {
            var converters = formatter.SerializerSettings.Converters;
            foreach (var converter in converters)
            {
                if (converter is AbpDateTimeConverter)
                {
                    var tmpConverter = converter as AbpDateTimeConverter;
                    tmpConverter.DateTimeFormat = dateTimeFormat;
                }
            }
        }
    }
}
