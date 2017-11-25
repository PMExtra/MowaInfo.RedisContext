using Microsoft.Extensions.DependencyInjection;
using MowaInfo.RedisContext.Core;

namespace MowaInfo.RedisContext.DependencyInjection
{
    internal class RedisContextBuilder : IRedisContextBuilder
    {
        private readonly Core.RedisContext _context;
        private readonly IServiceCollection _services;

        public RedisContextBuilder(IServiceCollection services, Core.RedisContext context)
        {
            _services = services;
            _context = context;
        }

        public IRedisContextBuilder AddObserver<T>() where T : RedisObserver, new()
        {
            _services.AddScoped(_ => _context.AddObserver<T>());
            return this;
        }

        public IRedisContextBuilder AddPublisher<T>() where T : RedisPublisher, new()
        {
            _services.AddScoped(_ => _context.AddPublisher<T>());
            return this;
        }
    }
}
