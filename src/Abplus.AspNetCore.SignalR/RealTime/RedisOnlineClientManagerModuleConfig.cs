using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abp.RealTime
{
    public class RedisOnlineClientManagerModuleConfig : IRedisOnlineClientManagerModuleConfig
    {
        public string ConnectionString { get; private set; }

        public string StoreKey { get; private set; }

        public RedisOnlineClientManagerModuleConfig()
        {
            ConnectionString = string.Empty;
            StoreKey = "Abplus.RealTime.OnlineClients";
        }

        public IRedisOnlineClientManagerModuleConfig ConnectTo(string connectionString)
        {
            ConnectionString = connectionString;
            return this;
        }

        public IRedisOnlineClientManagerModuleConfig WithStoreKey(string storeKey)
        {
            StoreKey = storeKey;
            return this;
        }
    }
}
