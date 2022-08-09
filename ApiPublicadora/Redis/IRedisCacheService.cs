using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPublicadora.Redis
{
    public interface IRedisCacheService
    {
        Task<T> BuscarCache<T>(string key);
        Task<RedisKey[]> ScanKeysAsync(string match, string count);
        Task<bool> SavarCacheAsync(string key, RedisValue value, TimeSpan expiry);
        Task<List<T>> BuscarCache<T>(RedisKey[] key);
    }
}
