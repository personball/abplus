using Abp;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Events.Bus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Abp.Runtime.Caching.Redis
{
    /// <summary>
    /// 移除清理缓存
    /// </summary>
    public class RedisCacheClear : ICacheClear, ITransientDependency
    {
        public void Clear(string fullkey, int databaseId = 0)
        {
            var _database = RedisManager._redis.GetDatabase(databaseId);
            _database.KeyDeleteWithPrefix(fullkey);
        }
    }
}
