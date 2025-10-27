

namespace Basket.API.Data;

public class BasketRepository(IDocumentSession session) : IBasketRepository
{
    public async Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken cancellationToken = default)
    {
        var basket = await session.LoadAsync<ShoppingCart>(userName, cancellationToken)
                     ?? throw new NotFoundException($"Basket not found with username {userName}");
        return basket;
    }

    public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart basket, CancellationToken cancellationToken = default)
    {
        session.Store(basket);
        await session.SaveChangesAsync(cancellationToken);
        return basket;

    }

    public async Task DeleteBasketAsync(string userName, CancellationToken cancellationToken = default)
    {
        session.Delete<ShoppingCart>(userName);
        await  session.SaveChangesAsync(cancellationToken);
    }
}