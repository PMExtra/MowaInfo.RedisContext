using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using StackExchange.Redis;

namespace MowaInfo.RedisContext
{
    public class RedisContext : IDisposable
    {
        private readonly Lazy<ConnectionMultiplexer> _lazyConnection;

        public RedisContext(string endPoint)
        {
            _lazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(endPoint));
            InitDatabaseProperties();
        }

        public RedisContext(ConfigurationOptions configuration)
        {
            _lazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(configuration));
            InitDatabaseProperties();
        }

        internal ConnectionMultiplexer Connection => _lazyConnection.Value;

        public ISubscriber Subscriber => Connection.GetSubscriber();

        public void Dispose()
        {
            if (_lazyConnection.IsValueCreated)
            {
                Connection.Dispose();
            }
        }

        internal static List<PropertyInfo> GetDatabaseProperties(Type t)
        {
            return t.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty)
                .Where(p => typeof(RedisDatabase).IsAssignableFrom(p.PropertyType))
                .ToList();
        }

        private void InitDatabaseProperties()
        {
            foreach (var property in GetDatabaseProperties(GetType()))
            {
                var database = InitDatabase(property);
                property.SetValue(this, database);
            }
        }

        internal RedisDatabase InitDatabase(PropertyInfo property)
        {
            var db = property.GetCustomAttribute<GetDatabaseAttribute>()?.Id
                     ?? property.PropertyType.GetTypeInfo().GetCustomAttribute<GetDatabaseAttribute>()?.Id
                     ?? -1;
            var database = (RedisDatabase)Activator.CreateInstance(property.PropertyType);
            database.Init(this, db);
            return database;
        }
    }
}
