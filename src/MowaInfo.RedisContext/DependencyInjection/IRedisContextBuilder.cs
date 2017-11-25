using System;
using MowaInfo.RedisContext.Core;

namespace MowaInfo.RedisContext.DependencyInjection
{
    public interface IRedisContextBuilder
    {
        IRedisContextBuilder AddObserver<T>() where T : RedisObserver, new();
        IRedisContextBuilder AddPublisher<T>() where T : RedisPublisher, new();
    }
}
