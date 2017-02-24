using System;
using System.Linq;

namespace Abplus.WebApiVersionRoute
{
    public class VersionRange
    {
        public VersionRange(string minVersionString, string maxVersionString)
            : this(new Version(minVersionString), new Version(maxVersionString))
        {
        }

        public VersionRange(Version min, Version max)
        {
            if (min == null)
            {
                throw new ArgumentNullException("min is null!");
            }

            if (max == null)
            {
                throw new ArgumentNullException("max is null!");
            }

            MinVersion = min;
            MaxVersion = max;

            ThrowIfMaxVersionLessThenMinVersion();
        }

        private void ThrowIfMaxVersionLessThenMinVersion()
        {
            if (MaxVersion.CompareTo(MinVersion) < 0)
            {
                throw new Exception("MaxVersion < MinVersion !");
            }
        }

        public Version MinVersion { get; private set; }

        public Version MaxVersion { get; private set; }

        public static VersionRange CreateVersionRangeFromString(string versionRangeString)
        {
            var minVersion = CreateMinVersionFromString(versionRangeString);

            var maxVersion = CreateMaxVersionFromString(versionRangeString);

            return new VersionRange(minVersion, maxVersion);
        }

        public static Version CreateMinVersionFromString(string versionRangeString)
        {
            if (string.IsNullOrWhiteSpace(versionRangeString))
            {
                throw new ArgumentNullException("versionRangeString is empty!");
            }

            var versionFirstToken = versionRangeString.Split('-').First();
            if (versionFirstToken == "*")
            {
                versionFirstToken = VersionConsts.DefaultClientMinVersionString;
            }

            return new Version(versionFirstToken);
        }

        public static Version CreateMaxVersionFromString(string versionRangeString)
        {
            if (string.IsNullOrWhiteSpace(versionRangeString))
            {
                throw new ArgumentNullException("versionRangeString is empty!");
            }

            var versionTokens = versionRangeString.Split('-');

            if (versionTokens.Length > 1)
            {
                if (versionTokens[1] == "*")
                {
                    return new Version(VersionConsts.DefaultClientMaxVersionString);
                }
                else
                {
                    return new Version(versionTokens[1]);
                }
            }
            else
            {
                return new Version(versionTokens[0]);
            }
        }
    }
}