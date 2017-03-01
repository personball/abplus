using System;
using System.Linq;

namespace Abp.Web.SimpleCaptcha
{
    public class SimpleCaptchaModuleConfig : ISimpleCaptchaModuleConfig
    {
        public SimpleCaptchaModuleConfig()
        {
            CodeReusable = false;
            TwistEnabled = false;
            RandomLineEnabled = true;
            RandomLineCount = 1;
            CaseSensitive = false;
            CharSetIncludeNumbers = true;
            CharSetIncludeUppercases = true;
            CharSetIncludeLowercases = false;
            CharSetExcluded = "01IOlo";
        }

        /// <summary>
        /// 是否扭曲，默认不扭曲
        /// </summary>
        public bool TwistEnabled { get; private set; }

        /// <summary>
        /// 随机线条，默认启用
        /// </summary>
        public bool RandomLineEnabled { get; private set; }

        /// <summary>
        /// 随机线条数量，默认1
        /// </summary>
        public int RandomLineCount { get; private set; }

        /// <summary>
        /// 是否大小写敏感, 默认false
        /// </summary>
        public bool CaseSensitive { get; private set; }

        /// <summary>
        /// 字符集是否包含小写字母，默认false
        /// </summary>
        public bool CharSetIncludeLowercases { get; private set; }

        /// <summary>
        /// 字符集是否包含大写字母，默认true
        /// </summary>
        public bool CharSetIncludeUppercases { get; private set; }

        /// <summary>
        /// 字符集是否包含数字，默认true
        /// </summary>
        public bool CharSetIncludeNumbers { get; private set; }

        /// <summary>
        /// 排除易混淆字符，默认"01IOlo"
        /// </summary>
        public string CharSetExcluded { get; private set; }

        /// <summary>
        /// 验证码是否可重复使用，默认false
        /// </summary>
        public bool CodeReusable { get; private set; }

        public ISimpleCaptchaModuleConfig EnableCodeReusable(bool enabled)
        {
            CodeReusable = enabled;
            return this;
        }

        public ISimpleCaptchaModuleConfig EnableTwist(bool enabled)
        {
            TwistEnabled = enabled;
            return this;
        }

        public ISimpleCaptchaModuleConfig EnableRandomLine(bool enabled)
        {
            RandomLineEnabled = enabled;
            return this;
        }

        public ISimpleCaptchaModuleConfig SetRandomLineCount(int count)
        {
            if (count < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(count), $"{nameof(count)} should greater than 0!");
            }

            RandomLineCount = count;
            return this;
        }

        public ISimpleCaptchaModuleConfig EnableCaseSensitive(bool caseSensitive)
        {
            CaseSensitive = caseSensitive;
            return this;
        }

        public ISimpleCaptchaModuleConfig IncludeCharSetLowercases(bool included)
        {
            CharSetIncludeLowercases = included;
            return this;
        }

        public ISimpleCaptchaModuleConfig IncludeCharSetUppercases(bool included)
        {
            CharSetIncludeUppercases = included;
            return this;
        }

        public ISimpleCaptchaModuleConfig IncludeCharSetNumbers(bool included)
        {
            CharSetIncludeNumbers = included;
            return this;
        }

        public ISimpleCaptchaModuleConfig ExcludeCharSet(params char[] excludedChars)
        {
            if (!excludedChars.Any())
            {
                CharSetExcluded = string.Empty;
            }

            CharSetExcluded = string.Join("", excludedChars);

            return this;
        }
    }
}
