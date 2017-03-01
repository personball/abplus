using Abp.Dependency;

namespace Abp.Web.SimpleCaptcha
{
    /// <summary>
    /// 
    /// </summary>
    public interface IVerificationCodeStore : ITransientDependency
    {

        void Save(string storeKey, string verificationCode);

        string Find(string storeKey);

        void Clear(string storeKey);
    }
}
