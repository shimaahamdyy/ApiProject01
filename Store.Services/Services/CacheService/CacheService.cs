using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.Services.Services.CacheService
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _database;

        public CacheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<string> GetCacheResponseAsync(string Key)
        {
            var cachResponse = await _database.StringGetAsync(Key);

            if (cachResponse.IsNullOrEmpty)
                return null;

            return cachResponse.ToString();
        }

        public async Task SetCacheResponseAsync(string Key, object response, TimeSpan timeForLive)
        {
            if (response is null)
                return;

            var option = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var serializedResponse = JsonSerializer.Serialize(response, option);

            await _database.StringSetAsync(Key, serializedResponse, timeForLive);
        }
    }
}
