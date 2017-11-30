using System;
using StackExchange.Redis;

namespace MowaInfo.RedisContext.Core
{
    public abstract class RedisObserver : IDisposable
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
                Subscriber.Subscribe(_channel, OnNext);
            }
        }

        private ISubscriber Subscriber => Context.Connection.GetSubscriber();

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
            Dispose(true);
            if (Context != null)
            {
                Subscriber.Unsubscribe(_channel, OnNext, CommandFlags.FireAndForget);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        protected abstract void OnNext(RedisChannel channel, RedisValue message);
    }
}
