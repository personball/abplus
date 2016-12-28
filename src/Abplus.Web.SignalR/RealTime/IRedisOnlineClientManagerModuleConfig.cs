using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abp.RealTime
{
    public interface IRedisOnlineClientManagerModuleConfig
    {
        /// <summary>
        /// Redis连接字符串
        /// </summary>
        string ConnectionString { get;  }

        /// <summary>
        /// 在线状态列表存储的键名
        /// </summary>
        string StoreKey { get; }


        IRedisOnlineClientManagerModuleConfig ConnectTo(string connectionString);

        IRedisOnlineClientManagerModuleConfig WithStoreKey(string storeKey);
    }
}
