using System;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Http;
using StackExchange.Redis;

namespace MowaInfo.RedisContext
{
    public class RedisContext : IDisposable
    {
        private readonly ConnectionMultiplexer _connection;

        public RedisContext(HostString host)
        {
            var addresses = Dns.GetHostAddressesAsync(host.Host).Result.Select(addr => addr.MapToIPv4());
            var configuration = host.Port == null
                ? string.Join(",", addresses)
                : string.Join(",", addresses.Select(addr => $"{addr}:{host.Port}"));
            _connection = ConnectionMultiplexer.Connect(configuration);
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }

        protected IDatabase GetDatabase(int db = -1)
        {
            return _connection.GetDatabase(db);
        }

        protected ISubscriber GetSubscriber()
        {
            return _connection.GetSubscriber();
        }
    }
}
