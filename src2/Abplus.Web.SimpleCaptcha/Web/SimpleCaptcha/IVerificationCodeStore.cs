namespace Abp.Web.SimpleCaptcha
{
    /// <summary>
    /// 验证码存储介质
    /// </summary>
    public interface IVerificationCodeStore
    {
        void Save(string storeKey, string verificationCode);

        string Find(string storeKey);

        void Clear(string storeKey);
    }
}
