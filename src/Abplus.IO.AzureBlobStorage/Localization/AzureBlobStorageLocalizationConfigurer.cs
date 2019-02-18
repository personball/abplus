using Abp.Configuration.Startup;
using Abp.IO;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace Abp.Localization
{
    public class AzureBlobStorageLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(AzureBlobStorageConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(AzureBlobStorageLocalizationConfigurer).GetAssembly(),
                        "Abp.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
