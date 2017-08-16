using System;
using System.Linq;
using System.Net;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using MowaInfo.RedisContext.Annotations;
using StackExchange.Redis;

namespace MowaInfo.RedisContext.Core
{
    public class RedisContext : IDisposable
    {
        private readonly Lazy<ConnectionMultiplexer> _lazyConnection;

        public RedisContext(HostString host)
        {
            var addresses = Dns.GetHostAddressesAsync(host.Host).Result.Select(addr => addr.MapToIPv4());
            var configuration = host.Port == null
                ? string.Join(",", addresses)
                : string.Join(",", addresses.Select(addr => $"{addr}:{host.Port}"));
            _lazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(configuration));
            InitDatabaseProperties();
        }

        public RedisContext(ConfigurationOptions configuration)
        {
            _lazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(configuration));
            InitDatabaseProperties();
        }

        internal ConnectionMultiplexer Connection => _lazyConnection.Value;

        public void Dispose()
        {
            if (_lazyConnection.IsValueCreated)
            {
                Connection.Dispose();
            }
        }

        private void InitDatabaseProperties()
        {
            var databaseProperties = GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty)
                .Where(p => p.PropertyType.IsAssignableFrom(typeof(RedisDatabase)))
                .ToList();

            foreach (var property in databaseProperties)
            {
                var dbId = property.GetCustomAttribute<GetDatabaseAttribute>()?.Id
                           ?? property.PropertyType.GetTypeInfo().GetCustomAttribute<GetDatabaseAttribute>()?.Id
                           ?? -1;
                var database = (RedisDatabase)Activator.CreateInstance(property.PropertyType);
                database.Init(this, dbId);
                property.SetValue(this, database);
            }
        }

        public T AddObserver<T>() where T : RedisObserver, new()
        {
            return (T)AddObserver(new T());
        }

        public RedisObserver AddObserver(RedisObserver observer)
        {
            observer.Init(this);
            return observer;
        }

        public SimpleObserver AddObserver(RedisChannel channel)
        {
            var observer = new SimpleObserver(channel);
            observer.Init(this);
            return observer;
        }

        public RedisPublisher AddPublisher<T>() where T : RedisPublisher, new()
        {
            return (T)AddPublisher(new T());
        }

        public RedisPublisher AddPublisher(RedisPublisher publisher)
        {
            publisher.Init(this);
            return publisher;
        }

        public SimplePublisher AddPublisher(RedisChannel channel)
        {
            var publisher = new SimplePublisher(channel);
            publisher.Init(this);
            return publisher;
        }
    }
}
