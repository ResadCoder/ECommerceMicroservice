using Catalog.API.DTOs.Products.Create.Handler;
using Catalog.API.Models;
using Shared.CQRS;

namespace Catalog.API.Products.CreateProduct;

internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommandDto,CreateProductResultDto>
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

        return new CreateProductResultDto(Guid.NewGuid());
    }
}