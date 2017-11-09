using System;
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
            _services.AddScoped(provider => _context.AddObserver<T>());
            return this;
        }

        public IRedisContextBuilder AddObserver<T>(Func<IServiceProvider, T> observerFactory) where T: RedisObserver
        {
            _services.AddScoped(provider =>
            {
                var observer =  observerFactory(provider);
                _context.AddObserver(observer);
                return observer;
            });
            return this;
        }

        public IRedisContextBuilder AddPublisher<T>() where T : RedisPublisher, new()
        {
            _services.AddScoped(provider => _context.AddPublisher<T>());
            return this;
        }

        public IRedisContextBuilder AddPublisher<T>(Func<IServiceProvider, T> publisherFactory) where T: RedisPublisher
        {
            _services.AddScoped(provider =>
            {
                var publisher = publisherFactory(provider);
                _context.AddPublisher(publisher);
                return publisher;
            });
            return this;
        }
    }
}
