namespace Basket.API.DTOs;

public record BasketCheckoutDto
{
    public string UserName { get; init; } = null!;
      
    public Guid CustomerId { get; init; } = Guid.Empty!;
      
    public decimal TotalPrice { get; init; } = default!;
      
    public string FirstName { get; init; } = null!;
      
    public string LastName { get; init; } = null!;
      
    public string Email { get; init; } = null!; 
      
    public string AddressLine { get; init; } = null!;
      
    public string Country { get; init; } = null!;
      
    public string State { get; init; } = null!;
      
    public string ZipCode { get; init; } = null!;
      
    public string CardName { get; init; } = null!;
      
    public string CardNumber { get; init; } = null!;
      
    public string Expiration { get; init; } = null!;
      
    public string Cvv { get; init; } = null!;
      
    public int PaymentMethod { get; init; } = default!;
    
}