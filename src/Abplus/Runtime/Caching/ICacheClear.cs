using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boss.EAM.Runtime.Caching
{
    public interface ICacheClear
    {
        void Clear(string fullkey, int databaseId = 0);
    }
}
