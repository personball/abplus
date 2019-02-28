using Abp;

namespace Sample.LocalFileSystem
{
    internal class SampleLocalFileSystemBootstrap
    {
        private static readonly AbpBootstrapper _bs = AbpBootstrapper.Create<SampleLocalFileSystemModule>();

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