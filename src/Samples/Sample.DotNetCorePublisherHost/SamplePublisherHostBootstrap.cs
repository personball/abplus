using Abp;

namespace Sample.DotNetCorePublisherHost
{
    public class SamplePublisherHostBootstrap
    {
        private static readonly AbpBootstrapper _bs = AbpBootstrapper.Create<SamplePublisherHostModule>();

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
