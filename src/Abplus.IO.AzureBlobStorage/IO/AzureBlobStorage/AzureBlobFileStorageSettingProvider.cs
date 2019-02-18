using System.Collections.Generic;
using Abp.Configuration;
using Abp.Localization;

namespace Abp.IO.AzureBlobStorage
{
    public class AzureBlobFileStorageSettingProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new List<SettingDefinition>
           {
               new SettingDefinition(
                    AzureBlobFileStorageSettingNames.AccountName,
                    "",
                    L("AzureBlobFileStorageAccountName"),
                    scopes: SettingScopes.Application|SettingScopes.Tenant),

               new SettingDefinition(
                   AzureBlobFileStorageSettingNames.AccountKey,
                   "",
                   L("AzureBlobFileStorageAccountKey"),
                   scopes:SettingScopes.Application|SettingScopes.Tenant),

               new SettingDefinition(
                   AzureBlobFileStorageSettingNames.Container,
                   "",
                   L("AzureBlobFileStorageContainer"),
                   scopes:SettingScopes.Application|SettingScopes.Tenant),

               new SettingDefinition(
                   AzureBlobFileStorageSettingNames.EndpointSuffix,
                   "",
                   L("AzureBlobFileStorageEndpointSuffix"),
                   scopes:SettingScopes.Application|SettingScopes.Tenant)
           };
        }

        private static LocalizableString L(string name)
        {
            return new LocalizableString(name, AzureBlobStorageConsts.LocalizationSourceName);
        }
    }
}
