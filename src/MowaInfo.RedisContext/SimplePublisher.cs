using System.Threading.Tasks;
using MowaInfo.RedisContext.Core;
using StackExchange.Redis;

namespace MowaInfo.RedisContext
{
    public class SimplePublisher : RedisPublisher
    {
        public SimplePublisher(RedisChannel channel)
        {
            Channel = channel;
        }

        public new long Publish(RedisValue message, CommandFlags flags = CommandFlags.None)
        {
            return base.Publish(message, flags);
        }

        public new async Task<long> PublishAsync(RedisValue message, CommandFlags flags = CommandFlags.None)
        {
            return await base.PublishAsync(message, flags);
        }
    }
}
