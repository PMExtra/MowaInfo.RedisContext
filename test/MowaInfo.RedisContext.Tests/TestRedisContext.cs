using StackExchange.Redis;

namespace MowaInfo.RedisContext.Tests
{
    public class TestRedisContext : RedisContext
    {
        public TestRedisContext(string endPoint) : base(endPoint)
        {
        }

        public TestRedisContext(ConfigurationOptions configuration) : base(configuration)
        {
        }

        [GetDatabase(1)]
        public TestRedisDatabase TestRedisDatabase { get; set; }
    }
}
