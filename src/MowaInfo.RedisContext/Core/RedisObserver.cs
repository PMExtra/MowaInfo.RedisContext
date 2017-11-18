using System;
using StackExchange.Redis;

namespace MowaInfo.RedisContext.Core
{
    public abstract class RedisObserver : IDisposable
    {
        private RedisChannel _channel;
        private RedisContext _context;

        protected RedisObserver(RedisChannel channel)
        {
            Channel = channel;
        }

        private ISubscriber Subscriber => _context.Connection.GetSubscriber();

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

        public void Dispose()
        {
            if (_context != null)
            {
                Subscriber.Unsubscribe(_channel, OnNext, CommandFlags.FireAndForget);
            }
        }

        protected abstract void OnNext(RedisChannel channel, RedisValue message);

        internal void Init(RedisContext context)
        {
            _context = context;
            Subscriber.Subscribe(Channel, OnNext);
        }
    }
}
