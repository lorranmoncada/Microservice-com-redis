using ApiPublicadora.Redis;
using MessageBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiPublicadora.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddMessageBuss(configuration.GetMessageQueueConnection("MessageBus"));
        }
    }

    public static class AddRedis
    {
        public static void AddRedisDataBaseConnection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(new ConnectionRedisCache(configuration));
        }
    }
}
