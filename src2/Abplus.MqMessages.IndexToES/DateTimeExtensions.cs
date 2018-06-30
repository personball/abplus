using System;

namespace Abp
{
    public static class DateTimeExtensions
    {
        public static string GetESIndexName(this DateTime now, string defaultIndexName, IndexFreq freqSetting)
        {
            Check.NotNullOrWhiteSpace(defaultIndexName, nameof(defaultIndexName));

            if (freqSetting == IndexFreq.PerDay)
            {
                return $"{defaultIndexName}-{DateTime.Now.Date.ToString("yyyy.MM.dd")}";
            }
            else if (freqSetting == IndexFreq.PerMonth)
            {
                return $"{defaultIndexName}-{DateTime.Now.Date.ToString("yyyy.MM")}";
            }
            else if (freqSetting == IndexFreq.PerYear)
            {
                return $"{defaultIndexName}-{DateTime.Now.Date.ToString("yyyy")}";
            }
            else
            {
                return defaultIndexName;
            }
        }
    }
}
