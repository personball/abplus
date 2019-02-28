using Abp;

namespace Sample.AzureBlobStorage
{
    internal class SampleAzureBlobStorageBootstrap
    {
        private static readonly AbpBootstrapper _bs = AbpBootstrapper.Create<SampleAzureBlobStorageModule>();

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