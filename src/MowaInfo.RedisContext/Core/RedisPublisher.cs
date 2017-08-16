using System.Threading.Tasks;
using StackExchange.Redis;

namespace MowaInfo.RedisContext.Core
{
    public abstract class RedisPublisher
    {
        private RedisContext _context;

        protected RedisPublisher(RedisChannel channel)
        {
            Channel = channel;
        }

        private ISubscriber Subscriber => _context.Connection.GetSubscriber();

        public RedisChannel Channel { get; }

        protected virtual long Publish(RedisValue message, CommandFlags flags = CommandFlags.None)
        {
            return Subscriber.Publish(Channel, message, flags);
        }

        protected virtual async Task<long> PublishAsync(RedisValue message, CommandFlags flags = CommandFlags.None)
        {
            return await Subscriber.PublishAsync(Channel, message, flags);
        }

        internal void Init(RedisContext context)
        {
            _context = context;
        }
    }
}
