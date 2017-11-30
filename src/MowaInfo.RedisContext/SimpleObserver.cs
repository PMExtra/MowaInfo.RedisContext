using System;
using MowaInfo.RedisContext.Core;
using StackExchange.Redis;

namespace MowaInfo.RedisContext
{
    public class SimpleObserver : RedisObserver
    {
        private readonly Action<RedisChannel, RedisValue> _onNext;

        public SimpleObserver(RedisChannel channel, Action<RedisChannel, RedisValue> onNext)
        {
            _onNext = onNext;
            Channel = channel;
        }

        protected override void OnNext(RedisChannel channel, RedisValue message)
        {
            _onNext.Invoke(channel, message);
        }
    }
}
