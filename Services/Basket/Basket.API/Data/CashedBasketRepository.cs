using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API.Data;

public class CashedBasketRepository(
    IBasketRepository basketRepository,
    IDistributedCache distributedCache
) : IBasketRepository
{
    public async Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken cancellationToken = default)
    {
        var cachedBasket = await distributedCache.GetStringAsync(userName, token: cancellationToken);
        if (!string.IsNullOrEmpty(cachedBasket))
            return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;

        // If cache miss, get from DB
        var basket = await basketRepository.GetBasketAsync(userName, cancellationToken);

        // 🧠 set expiration ONLY when adding new cache item
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
        };

        await distributedCache.SetStringAsync(
            userName,
            JsonSerializer.Serialize(basket),
            options,
            cancellationToken
        );

        return basket;
    }

    public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart basket, CancellationToken cancellationToken = default)
    {
        await basketRepository.StoreBasketAsync(basket, cancellationToken);

        // 🧠 set expiration when updating existing cache item too
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
        };

        await distributedCache.SetStringAsync(
            basket.UserName,
            JsonSerializer.Serialize(basket),
            options,
            cancellationToken
        );

        return basket;
    }

    public async Task DeleteBasketAsync(string userName, CancellationToken cancellationToken = default)
    {
        await basketRepository.DeleteBasketAsync(userName, cancellationToken);
        await distributedCache.RemoveAsync(userName, token: cancellationToken);
    }
}
