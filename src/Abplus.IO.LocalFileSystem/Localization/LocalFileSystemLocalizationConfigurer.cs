using Abp.Configuration.Startup;
using Abp.IO;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace Abp.Localization
{
    public  class LocalFileSystemLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(LocalFileSystemConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(LocalFileSystemLocalizationConfigurer).GetAssembly(),
                        "Abp.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
