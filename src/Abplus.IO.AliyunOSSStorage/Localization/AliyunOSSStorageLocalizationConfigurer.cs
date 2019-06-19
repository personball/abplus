using Abp.Configuration.Startup;
using Abp.IO;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace Abp.Localization
{
    public class AliyunOSSStorageLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(AliyunOSSStorageConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(AliyunOSSStorageLocalizationConfigurer).GetAssembly(),
                        "Abp.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
