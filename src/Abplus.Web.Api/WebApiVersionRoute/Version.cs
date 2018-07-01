using System;
using System.Collections.Generic;
using System.Linq;

namespace Abplus.WebApiVersionRoute
{
    public class Version : IComparable<Version>
    {
        public Version(string version)
        {
            VersionNumberList = new List<int>();
            version.Split(new string[] { "." }, StringSplitOptions.None).ToList()
                .ForEach((s) =>
                {
                    VersionNumberList.Add(int.Parse(s));
                });
        }

        public List<int> VersionNumberList { get; private set; }

        public int CompareTo(Version other)
        {
            if (this.VersionNumberList.Count == other.VersionNumberList.Count)
            {
                for (int i = 0; i < this.VersionNumberList.Count; i++)
                {
                    var thisVersionNumber = this.VersionNumberList[i];
                    var otherVersionNumber = other.VersionNumberList[i];

                    if (thisVersionNumber == otherVersionNumber)
                    {
                        continue;
                    }

                    if (thisVersionNumber < otherVersionNumber)
                    {
                        return -1;
                    }

                    if (thisVersionNumber > otherVersionNumber)
                    {
                        return 1;
                    }
                }

                return 0;
            }
            else
            {
                throw new Exception("version format not same!");
            }
        }
    }
}