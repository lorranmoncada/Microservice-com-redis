using StackExchange.Redis;

namespace ApiPublicadora.Redis
{
    public abstract class ConnectionRedisBase
    {
        public abstract IDatabase database { get; }
    }
}
