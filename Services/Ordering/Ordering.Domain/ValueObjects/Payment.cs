namespace Ordering.Domain.ValueObjects;

public record Payment
{
    public string? CardName { get;} = default!;
    
    public string? CardNumber { get;} = default!;
    
    public string? CardExpiration { get;} = default!;
    
    public string Cvv { get;} = default!;
    
    public int PaymentMethod { get;} = default!;
    
    protected  Payment()
    {
    }

    private Payment(string cardName, string cardNumber,
        string cardExpiration, string cvv, int paymentMethod)
    {
        CardName = cardName;
        CardNumber = cardNumber;
        CardExpiration = cardExpiration;
        Cvv = cvv;
        PaymentMethod = paymentMethod;
    }

    public static Payment Of(string cardName, string cardNumber,
        string cardExpiration, string cvv, int paymentMethod)
    {
        ArgumentException.ThrowIfNullOrEmpty(cardName);
        ArgumentException.ThrowIfNullOrEmpty(cardNumber);
        ArgumentException.ThrowIfNullOrEmpty(cvv);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(cvv.Length,3);
        
     return new Payment(cardName, cardNumber, cardExpiration, cvv, paymentMethod);
    }
    
}