using System;
using MowaInfo.RedisContext.Core;
using StackExchange.Redis;

namespace MowaInfo.RedisContext.Tests
{
    public class TestXmlObserver : RedisObserver
    {
        private readonly Action<RedisChannel, TestModel> _onNext;

        public TestXmlObserver(RedisChannel channel, Action<RedisChannel, TestModel> onNext)
        {
            _onNext = onNext;
            Channel = channel;
        }

        protected override void OnNext(RedisChannel channel, RedisValue message)
        {
            _onNext.Invoke(channel, XmlHelper.ParseFromXml<TestModel>(message));
        }
    }
}
