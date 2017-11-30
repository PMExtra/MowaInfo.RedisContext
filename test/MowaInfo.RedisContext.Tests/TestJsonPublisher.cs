using System.Threading.Tasks;
using MowaInfo.RedisContext.Core;
using StackExchange.Redis;

namespace MowaInfo.RedisContext.Tests
{
    public class TestJsonPublisher : RedisPublisher
    {
        public TestJsonPublisher(RedisChannel channel)
        {
            Channel = channel;
        }

        public long Publish(TestModel message, CommandFlags flags = CommandFlags.None)
        {
            return base.Publish(JsonHelper.ToJsonString(message), flags);
        }

        public Task<long> PublishAsync(TestModel message, CommandFlags flags = CommandFlags.None)
        {
            return base.PublishAsync(JsonHelper.ToJsonString(message), flags);
        }
    }
}
