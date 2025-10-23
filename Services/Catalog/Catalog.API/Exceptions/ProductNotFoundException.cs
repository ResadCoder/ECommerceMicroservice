using Shared.Exceptions;

namespace Catalog.API.Exceptions;

public class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException(string message) : base("Product not found")
    {
    }
}