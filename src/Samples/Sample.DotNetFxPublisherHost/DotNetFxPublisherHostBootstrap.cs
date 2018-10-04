using Abp;

namespace Sample.DotNetFxPublisherHost
{
    public class DotNetFxPublisherHostBootstrap
    {
        private static readonly AbpBootstrapper _bs = AbpBootstrapper.Create<DotNetFxPublisherHostModule>();

        public void Start()
        {
            _bs.Initialize();
        }

        public void Stop()
        {
            _bs.Dispose();
        }
    }
}
