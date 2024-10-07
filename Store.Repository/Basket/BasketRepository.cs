using StackExchange.Redis;
using Store.Repository.Basket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.Repository.Basket
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string BasketId)
            => await _database.KeyDeleteAsync(BasketId);

        public async Task<CustomerBasket> GetBasketAsync(string BasketId)
        {
            var basket = await _database.StringGetAsync(BasketId);

            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);

        }
        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket Basket)
        {
           var isCreated = await _database.StringSetAsync(Basket.Id , JsonSerializer.Serialize(Basket) ,TimeSpan.FromDays(30));

            if (!isCreated) 
                return null;

            return await GetBasketAsync(Basket.Id);
        }
    }
}
