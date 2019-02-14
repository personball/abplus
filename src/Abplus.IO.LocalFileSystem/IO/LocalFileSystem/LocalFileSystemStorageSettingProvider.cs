using System.Collections.Generic;
using Abp.Configuration;
using Abp.Localization;

namespace Abp.IO.LocalFileSystem
{
    public class LocalFileSystemStorageSettingProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new List<SettingDefinition>
            {
                new SettingDefinition(
                    LocalFileSystemStorageSettingNames.StoreRootDirectory,
                    "",
                    L("StoreRootDirectory"),
                    scopes: SettingScopes.Application),

                new SettingDefinition(
                    LocalFileSystemStorageSettingNames.AccessUriRootPath,
                    "",
                    L("AccessUriRootPath"),
                    scopes:SettingScopes.Application)
            };
        }

        private static LocalizableString L(string name)
        {
            return new LocalizableString(name, "AbplusIO");
        }
    }
}
