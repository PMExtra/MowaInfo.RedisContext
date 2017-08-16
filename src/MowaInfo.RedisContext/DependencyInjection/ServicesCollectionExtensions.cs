using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace MowaInfo.RedisContext.DependencyInjection
{
    public static class ServicesCollectionExtensions
    {
        public static IRedisContextBuilder AddRedisContext<T>(this IServiceCollection services)
            where T : Core.RedisContext, new()
        {
            var context = new T();
            services.AddSingleton(context);

            return new RedisContextBuilder(services, context);
        }

        public static IRedisContextBuilder AddRedisContext<T>(this IServiceCollection services, string host) where T : Core.RedisContext
        {
            var hostString = new HostString(host);
            var context = (T)Activator.CreateInstance(typeof(T), hostString);

            services.AddSingleton(context);
            services.AddDatabase(context);

            return new RedisContextBuilder(services, context);
        }

        public static IRedisContextBuilder AddRedisContext<T>(this IServiceCollection services, ConfigurationOptions configuration) where T : Core.RedisContext
        {
            var context = (T)Activator.CreateInstance(typeof(T), configuration);

            services.AddSingleton(context);
            services.AddDatabase(context);

            return new RedisContextBuilder(services, context);
        }

        private static void AddDatabase<T>(this IServiceCollection services, T context) where T : Core.RedisContext
        {
            foreach (var property in Core.RedisContext.GetDatabaseProperties(typeof(T)))
            {
                services.AddScoped(property.PropertyType, provider => context.InitDatabase(property));
            }
        }
    }
}
