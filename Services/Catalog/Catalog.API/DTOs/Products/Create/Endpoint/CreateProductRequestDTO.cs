namespace Catalog.API.DTOs.Products.Create.Endpoint;

public record CreateProductRequestDto
    (
        string Name,
        List<string> Categories,
        string Description,
        string ImageUrl,
        decimal Price
    );