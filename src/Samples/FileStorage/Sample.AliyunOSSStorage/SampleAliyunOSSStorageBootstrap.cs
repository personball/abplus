using Abp;

namespace Sample.AliyunOSSStorage
{
    internal class SampleAliyunOSSStorageBootstrap
    {
        private static readonly AbpBootstrapper _bs = AbpBootstrapper.Create<SampleAliyunOSSStorageModule>();

        public void Start()
        {
            //LogManager.Configuration = new XmlLoggingConfiguration("nlog.config");
            _bs.Initialize();
        }

        public void Stop()
        {
            _bs.Dispose();
        }
    }
}