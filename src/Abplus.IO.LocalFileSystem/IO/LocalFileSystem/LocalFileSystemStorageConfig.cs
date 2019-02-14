using Abp.Configuration;

namespace Abp.IO.LocalFileSystem
{
    public class LocalFileSystemStorageConfig : ILocalFileSystemStorageConfig
    {
        protected readonly ISettingManager SettingManager;

        public LocalFileSystemStorageConfig(ISettingManager settingManager)
        {
            SettingManager = settingManager;
        }

        public string StoreRootDirectory
        {
            get
            {
                return SettingManager.GetSettingValueForApplication(
                    LocalFileSystemStorageSettingNames.StoreRootDirectory);
            }
        }

        public string AccessUriRootPath
        {
            get
            {
                return SettingManager.GetSettingValueForApplication(
                    LocalFileSystemStorageSettingNames.AccessUriRootPath);
            }
        }
    }
}
