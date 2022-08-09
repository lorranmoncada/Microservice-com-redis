using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPublicadora.Redis
{
    public class RedisCacheService : IRedisCacheService
    {

        private readonly ConnectionRedisCache _connectionRedisCache;

        public RedisCacheService(ConnectionRedisCache connectionRedisCache)
        {
            _connectionRedisCache = connectionRedisCache;
        }
        public async Task<RedisKey[]> ScanKeysAsync(string match, string count)
        {
            var schemas = new List<RedisKey>();
            int nextCursor = 0;
            do
            {
                RedisResult redisResult = await _connectionRedisCache.database.ExecuteAsync("SCAN", nextCursor.ToString(), "MATCH", match, "COUNT", count);
                var innerResult = (RedisResult[])redisResult;

                nextCursor = int.Parse((string)innerResult[0]);

                var resultLines = ((RedisKey[])innerResult[1]).ToArray();
                schemas.AddRange(resultLines);
            }
            while (nextCursor != 0);

            return schemas.ToArray();
        }

        public async Task<List<T>> BuscarCache<T>(RedisKey[] key)
        {
            try
            {
                var result = await _connectionRedisCache.database.StringGetAsync(key);
               

                result = await _connectionRedisCache.database.StringGetAsync(key);

                var teste = new List<T>();
                foreach (var value in result.Where(x => x.HasValue))
                {
                    teste.Add(JsonConvert.DeserializeObject<T>(value));
                }

                return teste;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<T> BuscarCache<T>(string key)
        {
            try
            {
                var result = await _connectionRedisCache.database.StringGetAsync(key);
                if (string.IsNullOrEmpty(result))
                {
                    await _connectionRedisCache.database.StringSetAsync("teste",
                        JsonConvert.SerializeObject(new DebitoConta() { Valor = 50 }),
                        TimeSpan.FromSeconds(180));
                }

                result = await _connectionRedisCache.database.StringGetAsync(key);
                return JsonConvert.DeserializeObject<T>(result);



            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> SavarCacheAsync(string key, RedisValue value, TimeSpan expiry)
        {
            var result = await _connectionRedisCache.database.StringSetAsync(key, value, expiry);

            return result;
        }
    }
}
