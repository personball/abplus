using Abp;

namespace Sample.DotNetFxConsumerHost
{
    public class DotNetFxConsumerHostBootstrap
    {
        private static readonly AbpBootstrapper _bs = AbpBootstrapper.Create<DotNetFxConsumerHostModule>();

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
