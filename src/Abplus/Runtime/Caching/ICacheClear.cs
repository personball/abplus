using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abp.Runtime.Caching
{
    /// <summary>
    /// 移除清理缓存
    /// </summary>
    public interface ICacheClear
    {
        void Clear(string fullkey, int databaseId = 0);
    }
}
