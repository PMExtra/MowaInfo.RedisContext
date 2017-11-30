using System;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace MowaInfo.RedisContext.Core
{
    public abstract class RedisPublisher
    {
        private RedisChannel _channel;
        private RedisContext _context;

        internal RedisContext Context
        {
            get => _context;
            set
            {
                if (_context != null)
                {
                    throw new InvalidOperationException("Cannot be added repeatedly.");
                }
                _context = value;
            }
        }

        internal ISubscriber Subscriber => Context.Connection.GetSubscriber();

        public RedisChannel Channel
        {
            get => _channel;
            protected set
            {
                if (_context != null)
                {
                    throw new InvalidOperationException("The channel cannot be changed at work.");
                }
                _channel = value;
            }
        }

        protected virtual long Publish(RedisValue message, CommandFlags flags = CommandFlags.None)
        {
            return Subscriber.Publish(_channel, message, flags);
        }

        protected virtual async Task<long> PublishAsync(RedisValue message, CommandFlags flags = CommandFlags.None)
        {
            return await Subscriber.PublishAsync(_channel, message, flags);
        }
    }
}
