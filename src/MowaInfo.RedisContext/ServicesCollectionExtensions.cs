using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace MowaInfo.RedisContext
{
    public static class ServicesCollectionExtensions
    {
        public static IServiceCollection AddRedisContext<T>(this IServiceCollection services)
            where T : Core.RedisContext, new()
        {
            var context = new T();
            services.AddSingleton(context);

            return services;
        }

        public static IServiceCollection AddRedisContext<T>(this IServiceCollection services, string host) where T : Core.RedisContext
        {
            var hostString = new HostString(host);
            var context = (T)Activator.CreateInstance(typeof(T), hostString);

            services.AddSingleton(context);
            services.AddDatabase(context);

            return services;
        }

        public static IServiceCollection AddRedisContext<T>(this IServiceCollection services, ConfigurationOptions configuration) where T : Core.RedisContext
        {
            var context = (T)Activator.CreateInstance(typeof(T), configuration);

            services.AddSingleton(context);
            services.AddDatabase(context);

            return services;
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
