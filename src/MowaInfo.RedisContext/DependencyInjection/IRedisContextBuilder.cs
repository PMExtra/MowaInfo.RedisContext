using System;
using MowaInfo.RedisContext.Core;

namespace MowaInfo.RedisContext.DependencyInjection
{
    public interface IRedisContextBuilder
    {
        IRedisContextBuilder AddObserver<T>() where T : RedisObserver, new();
        IRedisContextBuilder AddObserver<T>(Func<IServiceProvider, T> observerFactory) where T : RedisObserver;
        IRedisContextBuilder AddPublisher<T>() where T : RedisPublisher, new();
        IRedisContextBuilder AddPublisher<T>(Func<IServiceProvider, T> publisherFactory) where T : RedisPublisher;
    }
}
