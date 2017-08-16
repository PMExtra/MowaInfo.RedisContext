using System;
using MowaInfo.RedisContext.Core;
using StackExchange.Redis;

namespace MowaInfo.RedisContext
{
    public class SimpleObserver : RedisObserver
    {
        public SimpleObserver(RedisChannel channel) : base(channel)
        {
        }

        public Action<RedisChannel, RedisValue> Handler { get; set; }

        protected override void OnNext(RedisChannel channel, RedisValue message)
        {
            Handler?.Invoke(channel, message);
        }
    }
}
