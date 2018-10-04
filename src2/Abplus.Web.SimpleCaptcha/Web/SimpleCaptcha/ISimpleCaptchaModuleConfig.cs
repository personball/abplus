using Abp.Dependency;

namespace Abp.Web.SimpleCaptcha
{
    public interface ISimpleCaptchaModuleConfig : ITransientDependency
    {
        #region Properties

        /// <summary>
        /// 使用cookie存储验证码时，必须提供16位加密密钥
        /// </summary>
        string CookieCodeStoreSecretKey { get; }

        /// <summary>
        /// 验证码有效期，单位：分钟
        /// </summary>
        int CodeExpiredInMinutes { get; }

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
        /// 设置验证码过期时长，默认20分钟
        /// </summary>
        /// <param name="minutes"></param>
        /// <returns></returns>
        ISimpleCaptchaModuleConfig SetMinutesCodeExpiredIn(int minutes);

        /// <summary>
        /// 验证码是否可多次使用，默认false，一旦验证通过立即清除
        /// </summary>
        /// <param name="enabled"></param>
        /// <returns></returns>
        ISimpleCaptchaModuleConfig EnableCodeReusable(bool enabled);

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

        /// <summary>
        /// 使用cookie存储验证码时，必须提供16位加密密钥
        /// </summary>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        ISimpleCaptchaModuleConfig SetCookieCodeStoreSecretKey(string secretKey);

        /// <summary>
        /// 使用Cookie存储验证码，默认使用Session
        /// 注意使用Cookie存储验证码时，必须提供长度为16位的加密密钥以对验证码值进行加密解密
        /// </summary>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        ISimpleCaptchaModuleConfig UseCookieCodeStore(string secretKey);

        /// <summary>
        /// 使用缓存存储验证码，默认使用session
        /// 注意使用缓存存储验证码时，验证码存储的StoreKey必须包含用户会话标识，
        /// 建议读取Key为ASP.NET_SessionId的Cookie值作为StoreKey的一部分。
        /// </summary>
        /// <returns></returns>
        ISimpleCaptchaModuleConfig UseCacheCodeStore();

        /// <summary>
        /// 使用session存储验证码
        /// </summary>
        /// <returns></returns>
        ISimpleCaptchaModuleConfig UseSessionCodeStore();

        #endregion
    }
}
