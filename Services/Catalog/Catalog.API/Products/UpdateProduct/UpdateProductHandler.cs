using Catalog.API.Exceptions;
using Catalog.API.Models;
using Marten;
using Shared.CQRS;

namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name, decimal Price, List<string> Categories)
    : ICommand<UpdateProductResult>;
        
public record UpdateProductResult(bool Success, string Message);        

public class UpdateProductHandler(IDocumentSession session, ILogger<UpdateProductHandler> logger): ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateProductHandler: UpdateProductCommand");
        
        var product = await session.LoadAsync<Product>(command.Id, cancellationToken)
            ?? throw new ProductNotFoundException("Product not found");
        product.Name = command.Name;
        product.Price = command.Price;
        product.Categories = command.Categories;
        
        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);
        
        return new UpdateProductResult(true, $"Product updated");
    }
}