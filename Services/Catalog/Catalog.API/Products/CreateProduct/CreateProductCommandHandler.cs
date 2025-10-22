using Catalog.API.Models;
using FluentValidation;
using Marten;
using Shared.CQRS;

namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommandDto
(
    string Name,
    List<string> Categories,
    string Description,
    string ImageUrl,
    decimal Price
) : ICommand<CreateProductResultDto>;

public record CreateProductResultDto
(
    Guid Id
);

public class CreateProductCommandDtoValidator : AbstractValidator<CreateProductCommandDto>
{
    public CreateProductCommandDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(50).WithMessage("Name must not exceed 50 characters");

        RuleFor(x => x.Categories)
            .NotEmpty().WithMessage("Category is required");

        RuleFor(x => x.ImageUrl)
            .NotEmpty().WithMessage("ImageUrl is required");
        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required")
            .MaximumLength(150).WithMessage("Description must not exceed 150 characters");
    }
}

internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommandDto,CreateProductResultDto>
{
    public async Task<CreateProductResultDto> Handle(CreateProductCommandDto command, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = command.Name,
            Categories = command.Categories,
            Description = command.Description,
            Price = command.Price,
            ImageUrl = command.ImageUrl
        };
        
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);

        return new CreateProductResultDto(product.Id);
    }
}