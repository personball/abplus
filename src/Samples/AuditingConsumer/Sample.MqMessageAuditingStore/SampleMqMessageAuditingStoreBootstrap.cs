using Abp;

namespace Sample.MqMessageAuditingStore
{
    public class SampleMqMessageAuditingStoreBootstrap
    {
        private static readonly AbpBootstrapper _bs = AbpBootstrapper.Create<SampleMqMessageAuditingStoreModule>();

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
