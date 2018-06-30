using System;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.MqMessages.MessageTrackers;
namespace Abp.MqMessages.MqHandlers
{
    public abstract class AbpMqHandlerBase : AbpServiceBase, ITransientDependency
    {
        public IMessageTracker MessageTracker { get; set; }

        public AbpMqHandlerBase(string localizationSourceName)
        {
            LocalizationSourceName = localizationSourceName;
            MessageTracker = DefaultInMemoryMessageTracker.Instance;
        }

        protected async Task<bool> IsTrueSetting(string settingKey)
        {
            return string.Equals("true", await SettingManager.GetSettingValueForApplicationAsync(settingKey), StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
