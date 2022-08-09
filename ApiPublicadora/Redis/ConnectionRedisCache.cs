using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace ApiPublicadora.Redis
{
    public sealed class ConnectionRedisCache : ConnectionRedisBase
    {
        public override IDatabase database { get; }

        public ConnectionRedisCache(IConfiguration configuration)
        {
            var connection = ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis"));
            database = connection.GetDatabase();
        }

    }
}
