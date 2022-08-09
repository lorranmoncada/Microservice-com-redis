using MessageBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiConsumidora.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddMessageBuss(configuration.GetMessageQueueConnection("MessageBus"));
        }
    }
}
