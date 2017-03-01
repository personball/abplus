using Abp.Dependency;

namespace Abp.Web.SimpleCaptcha
{
    public interface ISimpleCaptchaModuleConfig : ITransientDependency
    {
        #region Properties

        /// <summary>
        /// 验证码是否可重复使用，默认false
        /// </summary>
        bool CodeReusable { get; }

        /// <summary>
        /// 是否扭曲，默认不扭曲
        /// </summary>
        bool TwistEnabled { get; }

        /// <summary>
        /// 随机线条，默认启用
        /// </summary>
        bool RandomLineEnabled { get; }

        /// <summary>
        /// 随机线条数量，默认1
        /// </summary>
        int RandomLineCount { get; }

        /// <summary>
        /// 是否大小写敏感, 默认false
        /// </summary>
        bool CaseSensitive { get; }

        /// <summary>
        /// 字符集是否包含小写字母，默认false
        /// </summary>
        bool CharSetIncludeLowercases { get; }

        /// <summary>
        /// 字符集是否包含大写字母，默认true
        /// </summary>
        bool CharSetIncludeUppercases { get; }

        /// <summary>
        /// 字符集是否包含数字，默认true
        /// </summary>
        bool CharSetIncludeNumbers { get; }

        /// <summary>
        /// 排除易混淆字符，默认"01IOlo"
        /// </summary>
        string CharSetExcluded { get; }

        #endregion

        #region Methods
        /// <summary>
        /// 是否启用图片扭曲，默认不启用
        /// </summary>
        /// <returns></returns>
        ISimpleCaptchaModuleConfig EnableTwist(bool enabled);

        /// <summary>
        /// 是否启用随机线条，默认启用
        /// </summary>
        ISimpleCaptchaModuleConfig EnableRandomLine(bool enabled);

        /// <summary>
        /// 设置随机线条数量，默认1
        /// </summary>
        ISimpleCaptchaModuleConfig SetRandomLineCount(int count);

        /// <summary>
        /// 是否大小写敏感, 默认false
        /// </summary>
        ISimpleCaptchaModuleConfig EnableCaseSensitive(bool caseSensitive);

        /// <summary>
        /// 字符集是否包含小写字母，默认false
        /// </summary>
        ISimpleCaptchaModuleConfig IncludeCharSetLowercases(bool included);

        /// <summary>
        /// 字符集是否包含大写字母，默认true
        /// </summary>
        ISimpleCaptchaModuleConfig IncludeCharSetUppercases(bool included);

        /// <summary>
        /// 字符集是否包含数字，默认true
        /// </summary>
        ISimpleCaptchaModuleConfig IncludeCharSetNumbers(bool included);

        /// <summary>
        /// 排除易混淆字符，默认"01IOlo"
        /// </summary>
        ISimpleCaptchaModuleConfig ExcludeCharSet(params char[] excludedChars);

        #endregion
    }
}
