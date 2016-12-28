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
    public class RedisOnlineClientManager : IOnlineClientManager
    {
        public event EventHandler<OnlineClientEventArgs> ClientConnected;
        public event EventHandler<OnlineClientEventArgs> ClientDisconnected;
        public event EventHandler<OnlineUserEventArgs> UserConnected;
        public event EventHandler<OnlineUserEventArgs> UserDisconnected;

        private readonly string _connectionString;
        private readonly string _storeKey;
        private readonly string _clientStoreKey;
        private readonly string _userStoreKey;

        private readonly Lazy<ConnectionMultiplexer> _connectionMultiplexer;

        private readonly object _syncObj = new object();

        public ILogger Logger { get; set; }

        public RedisOnlineClientManager()
        {
            var config = IocManager.Instance.Resolve<IRedisOnlineClientManagerModuleConfig>();
            if (config == null || config.ConnectionString.IsNullOrWhiteSpace() || config.StoreKey.IsNullOrWhiteSpace())
            {
                throw new Exception("RedisOnlineClientManagerModuleConfig is invalid!");
            }

            _connectionString = config.ConnectionString;
            _storeKey = config.StoreKey;
            _clientStoreKey = _storeKey + ".clients";
            _userStoreKey = _storeKey + ".users";
            Logger = NullLogger.Instance;

            _connectionMultiplexer = new Lazy<ConnectionMultiplexer>(CreateConnectionMultiplexer);
        }

        private ConnectionMultiplexer CreateConnectionMultiplexer()
        {
            return ConnectionMultiplexer.Connect(_connectionString);
        }

        protected IDatabase GetDatabase()
        {
            return _connectionMultiplexer.Value.GetDatabase();
        }

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

            var userClientsValue = _database.HashGet(_userStoreKey, userId.ToUserIdentifierString());
            if (userClientsValue.IsNullOrEmpty)
            {
                _database.HashDelete(_userStoreKey, userId.ToUserIdentifierString());
            }

            var userClients = JsonConvert.DeserializeObject<List<string>>(userClientsValue);
            if (userClients.Contains(client.ConnectionId))
            {
                return;
            }

            userClients.Add(client.ConnectionId);
            _database.HashSet(_storeKey, new HashEntry[] { new HashEntry(userId.ToUserIdentifierString(), userClients.ToJsonString()) });
        }

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

        public bool Remove(string connectionId)
        {
            lock (_syncObj)
            {
                var isRemoved = false;
                var _database = GetDatabase();
                var clientValue = _database.HashGet(_clientStoreKey, connectionId);
                if (clientValue.IsNullOrEmpty)
                {
                    return isRemoved;
                }

                _database.HashDelete(_clientStoreKey, connectionId);
                isRemoved = true;

                var client = JsonConvert.DeserializeObject<OnlineClient>(clientValue);
                
                if (isRemoved)
                {
                    var user = client.ToUserIdentifierOrNull();

                    if (user != null && !IsUserOnline(user))
                    {
                        UserDisconnected.InvokeSafely(this, new OnlineUserEventArgs(user, client));
                    }

                    ClientDisconnected.InvokeSafely(this, new OnlineClientEventArgs(client));
                }

                return isRemoved;
            }
        }
    }
}
