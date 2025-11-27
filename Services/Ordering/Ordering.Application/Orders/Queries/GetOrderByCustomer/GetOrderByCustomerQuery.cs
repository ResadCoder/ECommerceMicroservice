using FluentValidation;
using Ordering.Application.DTOs;
using Shared.CQRS;

namespace Ordering.Application.Orders.Queries.GetOrderByCustomer;

public record GetOrdersByCustomerQuery(Guid CustomerId) : IQuery<GetOrdersByCustomerResult>;

public record GetOrdersByCustomerResult(IEnumerable<OrderDto> Orders);

public class GetOrdersByCustomerQueryValidator : AbstractValidator<GetOrdersByCustomerQuery>
{
    public GetOrdersByCustomerQueryValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty().WithMessage("CustomerId is required.");
    }
}

