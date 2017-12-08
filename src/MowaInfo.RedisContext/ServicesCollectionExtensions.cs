using System;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace MowaInfo.RedisContext
{
    public static class ServicesCollectionExtensions
    {
        public static IServiceCollection AddRedisContext<T>(this IServiceCollection services)
            where T : RedisContext, new()
        {
            var context = new T();
            services.AddSingleton(context);

            return services;
        }

        public static IServiceCollection AddRedisContext<T>(this IServiceCollection services, string endPoint) where T : RedisContext
        {
            var context = (T)Activator.CreateInstance(typeof(T), endPoint);

            services.AddSingleton(context);
            services.AddDatabase(context);

            return services;
        }

        public static IServiceCollection AddRedisContext<T>(this IServiceCollection services, ConfigurationOptions configuration) where T : RedisContext
        {
            var context = (T)Activator.CreateInstance(typeof(T), configuration);

            services.AddSingleton(context);
            services.AddDatabase(context);

            return services;
        }

        private static void AddDatabase<T>(this IServiceCollection services, T context) where T : RedisContext
        {
            foreach (var property in RedisContext.GetDatabaseProperties(typeof(T)))
            {
                services.AddScoped(property.PropertyType, provider => context.InitDatabase(property));
            }
        }
    }
}
