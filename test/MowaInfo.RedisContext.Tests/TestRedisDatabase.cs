using System.Threading.Tasks;
using StackExchange.Redis;

namespace MowaInfo.RedisContext.Tests
{
    public class TestRedisDatabase : RedisDatabase
    {
        public async Task<RedisValue> TestGetAsync(string key)
        {
            return await Database.StringGetAsync(key);
        }

        public Task<bool> TestSetAsync(string key, string value)
        {
            return Database.StringSetAsync(key, value);
        }
    }
}
