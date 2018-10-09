using Abp;

namespace Sample.AuditingConsumerHandler
{
    public class SampleAuditingConsumerHandlerBootstrap
    {
        private static readonly AbpBootstrapper _bs = AbpBootstrapper.Create<SampleAuditingConsumerHandlerModule>();

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
