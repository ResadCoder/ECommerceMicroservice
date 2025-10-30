using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Context.Extensions;

internal class InitialData
{
    public static IEnumerable<Customer> Customers => new List<Customer>
    {
        Customer.Create(CustomerId.Of(new Guid("c1f49a4a-4b57-4b4d-83e3-8ce6c7f4a0e5")), "Rashad", "resadsadiqov62@gmail.com"),
        Customer.Create(CustomerId.Of(new Guid("8dfc6de2-8471-4b91-82f0-3a05d8bde05f")), "Emin", "eminsadiqov354@gmail.com")
    };

    public static IEnumerable<Product> Products => new List<Product>
    {
        Product.Create(ProductId.Of(new Guid("f27b8e6e-0e02-4b3a-9a91-5a0a2bb7de74")), "Wireless Mouse", 25.99m),
        Product.Create(ProductId.Of(new Guid("f91cf287-8301-46e2-9a50-5b8e80b6b331")), "Mechanical Keyboard", 89.50m)
    };

    public static IEnumerable<Order> Orders => new List<Order>
    {
        Order.Create(
            OrderId.Of(new Guid("6fcb3e7c-6d92-4e0e-b4e5-4db18b8c2a42")), 
            ProductId.Of(new Guid("f27b8e6e-0e02-4b3a-9a91-5a0a2bb7de74")), 
            CustomerId.Of(new Guid("c1f49a4a-4b57-4b4d-83e3-8ce6c7f4a0e5")), 
            OrderName.Of("Setup"),
            Address.Of(
                firstName: "Rashad",
                lastName: "Sadiqov",
                emailAddress: "resadsadiqov62@gmail.com",
                addressLine: "Nizami Street 12",
                country: "Azerbaijan",
                state: "Baku",
                zipCode: "AZ100"  // shortened to 5 chars
            ),
            Address.Of(
                firstName: "Rashad",
                lastName: "Sadiqov",
                emailAddress: "resadsadiqov62@gmail.com",
                addressLine: "Nizami Street 12",
                country: "Azerbaijan",
                state: "Baku",
                zipCode: "AZ100"  // shortened to 5 chars
            ),
            Payment.Of(
                cardName: "Rashad Sadiqov",
                cardNumber: "4111111111111111",
                cardExpiration: "12/27",
                cvv: "123",
                paymentMethod: 1
            )
        ),
        Order.Create(
            OrderId.Of(new Guid("f129b8d5-2b78-4a6a-9c79-0672b55f3a9a")), 
            ProductId.Of(new Guid("f91cf287-8301-46e2-9a50-5b8e80b6b331")), 
            CustomerId.Of(new Guid("8dfc6de2-8471-4b91-82f0-3a05d8bde05f")),
            OrderName.Of("Homes"),
            Address.Of(
                firstName: "Emin",
                lastName: "Sadiqov",
                emailAddress: "eminsadiqov354@gmail.com",
                addressLine: "Heydar Aliyev Ave 45",
                country: "Azerbaijan",
                state: "Ganja",
                zipCode: "AZ200" // shortened to 5 chars
            ),
            Address.Of(
                firstName: "Emin",
                lastName: "Sadiqov",
                emailAddress: "eminsadiqov354@gmail.com",
                addressLine: "Heydar Aliyev Ave 45",
                country: "Azerbaijan",
                state: "Ganja",
                zipCode: "AZ200" // shortened to 5 chars
            ),
            Payment.Of(
                cardName: "Emin Sadiqov",
                cardNumber: "5555555555554444",
                cardExpiration: "09/28",
                cvv: "456",
                paymentMethod: 2
            )
        )
    };
}
