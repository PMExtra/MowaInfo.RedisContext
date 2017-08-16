using System;
using StackExchange.Redis;

namespace MowaInfo.RedisContext.Core
{
    public abstract class RedisObserver : IDisposable
    {
        private RedisContext _context;

        protected RedisObserver(RedisChannel channel)
        {
            Channel = channel;
        }

        private ISubscriber Subscriber => _context.Connection.GetSubscriber();

        public RedisChannel Channel { get; }

        public void Dispose()
        {
        }

        protected abstract void OnNext(RedisChannel channel, RedisValue message);

        internal void Init(RedisContext context)
        {
            _context = context;
            Subscriber.Subscribe(Channel, OnNext);
        }
    }
}
