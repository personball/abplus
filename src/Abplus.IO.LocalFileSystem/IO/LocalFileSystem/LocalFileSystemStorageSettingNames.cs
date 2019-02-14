namespace Abp.IO.LocalFileSystem
{
    public static class LocalFileSystemStorageSettingNames
    {
        /// <summary>
        /// 存储的根目录，系统目录（注意不同OS的路径表示方式）
        /// </summary>
        public const string StoreRootDirectory = "Abplus.IO.LocalSystemFileStorage.StoreRootDirectory";

        /// <summary>
        /// 读取的URI路径
        /// </summary>
        public const string AccessUriRootPath = "Abplus.IO.LocalSystemFileStorage.AccessUriRootPath";
    }
}
