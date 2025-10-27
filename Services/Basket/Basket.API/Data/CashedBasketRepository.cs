using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API.Data;

public class CashedBasketRepository(IBasketRepository basketRepository,
    IDistributedCache distributedCache
    ) : IBasketRepository
{
    public async Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken cancellationToken = default)
    {
        var cashedBasket = await distributedCache.GetStringAsync(userName, token: cancellationToken);
        if (!string.IsNullOrEmpty(cashedBasket))
           return  JsonSerializer.Deserialize<ShoppingCart>(cashedBasket)!;
        
        var basket = await basketRepository.GetBasketAsync(userName, cancellationToken);
        await distributedCache.SetStringAsync(userName, JsonSerializer.Serialize(basket), token: cancellationToken);
        
        return basket;
    }

    public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart basket, CancellationToken cancellationToken = default)
    {
         await basketRepository.StoreBasketAsync(basket, cancellationToken);
        
         await distributedCache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), token: cancellationToken);
         
         return basket;
    }

    public async Task DeleteBasketAsync(string userName, CancellationToken cancellationToken = default)
    {
        await basketRepository.DeleteBasketAsync(userName, cancellationToken);
        
        await distributedCache.RemoveAsync(userName, token: cancellationToken);
    }
}