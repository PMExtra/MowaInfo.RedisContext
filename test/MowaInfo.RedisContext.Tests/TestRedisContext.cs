using Microsoft.AspNetCore.Http;
using MowaInfo.RedisContext.Annotations;
using StackExchange.Redis;

namespace MowaInfo.RedisContext.Tests
{
    public class TestRedisContext : Core.RedisContext
    {
        public TestRedisContext(HostString host) : base(host)
        {
        }

        public TestRedisContext(ConfigurationOptions configuration) : base(configuration)
        {
        }

        [GetDatabase(1)]
        public TestRedisDatabase TestRedisDatabase { get; set; }
    }
}
