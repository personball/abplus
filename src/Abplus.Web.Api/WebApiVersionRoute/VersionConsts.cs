namespace Abplus.WebApiVersionRoute
{
    public class VersionConsts
    {
        public const string ClientVersionHeaderName = "Abplus-ClientVersion";
        public const string VersionHeaderName = "Abplus-ApiVersion";
        public const string SysCodeHeaderName = "Abplus-SysCode";

        public const string DefaultClientMinVersionString = "1.0.0";
        public const string DefaultClientMaxVersionString = "9999.9999.9999";

        public const int DefaultApiMinVersion = 1;
    }
}
