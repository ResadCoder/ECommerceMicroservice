using Catalog.API.Exceptions;
using Catalog.API.Models;
using FluentValidation;
using Marten;
using Shared.CQRS;

namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name, decimal Price, List<string> Categories)
    : ICommand<UpdateProductResult>;
        
public record UpdateProductResult(bool Success, string Message);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(50).WithMessage("Name must not exceed 50 characters");
        RuleFor(x => x.Price)
            .NotEmpty().WithMessage("Price is required")
            .GreaterThan(0).WithMessage("Price must be greater than 0");
        RuleFor(x => x.Categories)
            .NotEmpty()
            .WithMessage("Categories is required");
    }
}

public class UpdateProductHandler(IDocumentSession session): ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        
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