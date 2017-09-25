using MowaInfo.RedisContext.Core;

namespace MowaInfo.RedisContext.DependencyInjection
{
    public interface IRedisContextBuilder
    {
        IRedisContextBuilder AddObserver<T>() where T : RedisObserver, new();
        IRedisContextBuilder AddObserver(RedisObserver observer);
        IRedisContextBuilder AddPublisher<T>() where T : RedisPublisher, new();
        IRedisContextBuilder AddPublisher(RedisPublisher publisher);
    }
}
