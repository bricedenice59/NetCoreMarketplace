using System.Text.Json;
using Core.Interfaces;
using Core.Models;
using StackExchange.Redis;

namespace Infrastructure;

public class BasketRepository : IBasketRepository
{
    private readonly IDatabase _database;
    
    public BasketRepository(IConnectionMultiplexer connectionMultiplexer)
    {
        _database = connectionMultiplexer.GetDatabase();
    }
    
    public async Task<CustomerBasket?> GetCustomerBasket(Guid basketId)
    {
        var data = await _database.StringGetAsync(basketId.ToString());
        return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);
    }

    public async Task<CustomerBasket?> UpdateCustomerBasket(CustomerBasket basket)
    {
        var created = await _database.StringSetAsync(basket.Id.ToString(), 
            JsonSerializer.Serialize(basket), 
            TimeSpan.FromDays(2));
        return !created ? null : await GetCustomerBasket(basket.Id);
    }

    public async Task<bool> DeleteCustomerBasket(Guid basketId)
    {
        return await _database.KeyDeleteAsync(basketId.ToString());
    }
}