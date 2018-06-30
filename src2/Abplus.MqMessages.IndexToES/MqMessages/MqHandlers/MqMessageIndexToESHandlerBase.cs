using System;
using System.Threading.Tasks;
using Abp.Dependency;
using Nest;
using Rebus.Handlers;

namespace Abp.MqMessages.MqHandlers
{
    public abstract class MqMessageIndexToESHandlerBase<TMqMessage> : AbpMqHandlerBase
        , IHandleMessages<TMqMessage>
        , ITransientDependency where TMqMessage : class
    {

        protected string DefaultIndexName { get; set; }

        protected IndexFreq IndexFreq { get; set; }

        public IElasticClient ElasticClient { get; set; }

        public MqMessageIndexToESHandlerBase(string defaultIndexName, IndexFreq indexFreq, string localizationSourceName)
            : base(localizationSourceName)
        {
            DefaultIndexName = defaultIndexName;
            IndexFreq = indexFreq;
        }

        public async Task Handle(TMqMessage message)
        {
            var indexName = CustomIndexName();
            var indexFreq = CustomIndexFreq();

            await ElasticClient.IndexAsync(message, i => i.Index(DateTime.Now.GetESIndexName(indexName, indexFreq)));
        }

        public virtual string CustomIndexName()
        {
            return DefaultIndexName;
        }

        public virtual IndexFreq CustomIndexFreq()
        {
            return IndexFreq;
        }
    }
}
