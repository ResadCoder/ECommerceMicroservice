using Catalog.API.Exceptions;
using Catalog.API.Models;
using Marten;
using Shared.CQRS;

namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductCommand(Guid ProductId) : ICommand<DeleteProductResult>;
public record DeleteProductResult(bool IsSuccess);    


public class DeleteProductHandler(IDocumentSession session): ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(command.ProductId, cancellationToken)
            ?? throw new ProductNotFoundException("Product not found");
        
        session.Delete(product);
        await session.SaveChangesAsync(cancellationToken);
        return new DeleteProductResult(true);
    }
}