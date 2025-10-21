using Catalog.API.DTOs.Products.Create.Handler;
using Shared.CQRS;

namespace Catalog.API.DTOs.Products.Create.Handler;

public record CreateProductCommandDto
    (
        string Name,
        List<string> Categories,
        string Description,
        string ImageUrl,
        decimal Price
    ) : ICommand<CreateProductResultDto>;