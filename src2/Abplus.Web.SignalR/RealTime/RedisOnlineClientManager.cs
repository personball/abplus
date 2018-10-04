using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Abp.Dependency;
using Abp.Extensions;
using Abp.Json;
using Castle.Core.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Abp.RealTime
{
    /// <summary>
    /// 基于Redis的OnlineClientManager
    /// </summary>
    public class RedisOnlineClientManager : IOnlineClientManager
    {
        /// <summary>
        /// Client 连接成功时
        /// </summary>
        public event EventHandler<OnlineClientEventArgs> ClientConnected;
        /// <summary>
        /// Client断开连接时
        /// </summary>
        public event EventHandler<OnlineClientEventArgs> ClientDisconnected;
        /// <summary>
        /// 用户连接成功时
        /// </summary>
        public event EventHandler<OnlineUserEventArgs> UserConnected;
        /// <summary>
        /// 用户断开连接时
        /// </summary>
        public event EventHandler<OnlineUserEventArgs> UserDisconnected;

        private readonly string _connectionString;
        private readonly string _storeKey;
        private readonly string _clientStoreKey;
        private readonly string _userStoreKey;

        private readonly Lazy<ConnectionMultiplexer> _connectionMultiplexer;

        private readonly object _syncObj = new object();

        /// <summary>
        /// 日志
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// ctor
        /// </summary>
        public RedisOnlineClientManager()
        {
            var config = IocManager.Instance.Resolve<IRedisOnlineClientManagerModuleConfig>();
            if (config == null || config.ConnectionString.IsNullOrWhiteSpace() || config.StoreKey.IsNullOrWhiteSpace())
            {
                throw new Exception("RedisOnlineClientManagerModuleConfig is invalid!");
            }

            _connectionString = config.ConnectionString;
            _storeKey = config.StoreKey;
            _clientStoreKey = _storeKey + ".Clients";
            _userStoreKey = _storeKey + ".Users";
            Logger = NullLogger.Instance;

            _connectionMultiplexer = new Lazy<ConnectionMultiplexer>(CreateConnectionMultiplexer);
        }

        private ConnectionMultiplexer CreateConnectionMultiplexer()
        {
            return ConnectionMultiplexer.Connect(_connectionString);
        }

        /// <summary>
        /// 获取Redis Database
        /// </summary>
        /// <returns></returns>
        protected IDatabase GetDatabase()
        {
            return _connectionMultiplexer.Value.GetDatabase();
        }

        /// <summary>
        /// 添加Client
        /// </summary>
        /// <param name="client"></param>
        public void Add(IOnlineClient client)
        {
            lock (_syncObj)
            {
                var userWasAlreadyOnline = false;
                var user = client.ToUserIdentifierOrNull();

                if (user != null)
                {
                    userWasAlreadyOnline = IsUserOnline(user);
                }

                AddClientToRedisStore(client);

                ClientConnected.InvokeSafely(this, new OnlineClientEventArgs(client));

                if (user != null && !userWasAlreadyOnline)
                {
                    UserConnected.InvokeSafely(this, new OnlineUserEventArgs(user, client));
                }
            }
        }

        private bool IsUserOnline(UserIdentifier user)
        {
            var _database = GetDatabase();
            return _database.HashExists(_userStoreKey, user.ToUserIdentifierString());
        }

        private void AddClientToRedisStore(IOnlineClient client)
        {
            var _database = GetDatabase();
            _database.HashSet(_clientStoreKey, new HashEntry[] { new HashEntry(client.ConnectionId, client.ToString()) });
            var userId = client.ToUserIdentifierOrNull();
            if (userId == null)
            {
                return;
            }

            var userClients = new List<string>();
            var userClientsValue = _database.HashGet(_userStoreKey, userId.ToUserIdentifierString());
            if (userClientsValue.HasValue)
            {
                userClients = JsonConvert.DeserializeObject<List<string>>(userClientsValue);
            }

            if (userClients.Contains(client.ConnectionId))
            {
                return;
            }

            userClients.Add(client.ConnectionId);
            _database.HashSet(_userStoreKey, new HashEntry[] { new HashEntry(userId.ToUserIdentifierString(), userClients.ToJsonString()) });
        }

        /// <summary>
        /// 获取所有Clients
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<IOnlineClient> GetAllClients()
        {
            lock (_syncObj)
            {
                var _database = GetDatabase();
                var clientsEntries = _database.HashGetAll(_clientStoreKey);
                var clients = new List<IOnlineClient>();
                foreach (var entry in clientsEntries)
                {
                    clients.Add(JsonConvert.DeserializeObject<OnlineClient>(entry.Value));
                }

                return clients.ToImmutableList();
            }
        }

        /// <summary>
        /// 根据连接id获取client
        /// </summary>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public IOnlineClient GetByConnectionIdOrNull(string connectionId)
        {
            lock (_syncObj)
            {
                var _database = GetDatabase();
                var clientValue = _database.HashGet(_clientStoreKey, connectionId);
                if (clientValue.IsNullOrEmpty)
                {
                    return null;
                }

                return JsonConvert.DeserializeObject<OnlineClient>(clientValue);
            }
        }

        /// <summary>
        /// 移除Client
        /// </summary>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public bool Remove(string connectionId)
        {
            lock (_syncObj)
            {
                var _database = GetDatabase();
                var clientValue = _database.HashGet(_clientStoreKey, connectionId);
                if (clientValue.IsNullOrEmpty)
                {
                    return true;
                }

                var client = JsonConvert.DeserializeObject<OnlineClient>(clientValue);
                var user = client.ToUserIdentifierOrNull();
                if (user != null)
                {
                    //从_userStoreKey中移除一个client
                    var userClientsValue = _database.HashGet(_userStoreKey, user.ToUserIdentifierString());
                    if (userClientsValue.HasValue)
                    {
                        var userClients = JsonConvert.DeserializeObject<List<string>>(userClientsValue);
                        userClients.Remove(connectionId);
                        if (userClients.Count > 0)
                        {
                            //更新
                            _database.HashSet(_userStoreKey, new HashEntry[] { new HashEntry(user.ToUserIdentifierString(), userClients.ToJsonString()) });
                        }
                        else
                        {
                            //删除
                            _database.HashDelete(_userStoreKey, user.ToUserIdentifierString());
                        }
                    }

                    _database.HashDelete(_clientStoreKey, connectionId);

                    if (!IsUserOnline(user))
                    {
                        UserDisconnected.InvokeSafely(this, new OnlineUserEventArgs(user, client));
                    }
                }

                ClientDisconnected.InvokeSafely(this, new OnlineClientEventArgs(client));
                return true;
            }
        }

        /// <summary>
        /// 获取指定user的所有clients
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IReadOnlyList<IOnlineClient> GetAllByUserId(IUserIdentifier user)
        {
            var clients = new List<OnlineClient>();

            var userIdentifier = new UserIdentifier(user.TenantId, user.UserId);
            if (!IsUserOnline(userIdentifier))
            {
                return clients;
            }

            lock (_syncObj)
            {
                var _database = GetDatabase();

                var userClients = new List<string>();
                var userClientsValue = _database.HashGet(_userStoreKey, userIdentifier.ToUserIdentifierString());
                if (userClientsValue.HasValue)
                {
                    userClients = JsonConvert.DeserializeObject<List<string>>(userClientsValue);
                    foreach (var connectionId in userClients)
                    {
                        var clientValue = _database.HashGet(_clientStoreKey, connectionId);
                        if (clientValue.IsNullOrEmpty)
                        {
                            continue;
                        }

                        clients.Add(JsonConvert.DeserializeObject<OnlineClient>(clientValue));
                    }
                }
            }

            return clients;
        }
    }
}
