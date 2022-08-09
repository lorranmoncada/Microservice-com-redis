using Microsoft.Extensions.DependencyInjection;
using System;

namespace MessageBus
{
    public static class MessageBusDependencyInjection
    {
        public static IServiceCollection AddMessageBuss(this IServiceCollection services, string connection)
        {
            if (string.IsNullOrEmpty(connection)) throw new ArgumentNullException();

            services.AddSingleton<IMessageBus>(new MessageBus(connection));

            return services;
        }
    }
}
