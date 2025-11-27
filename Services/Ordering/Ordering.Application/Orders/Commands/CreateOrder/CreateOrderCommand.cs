using FluentValidation;
using Ordering.Application.DTOs;
using Shared.CQRS;

namespace Ordering.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(OrderDto Dto)
        : ICommand<CreateOrderResult>;

public record CreateOrderResult(Guid Id);


public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
        public CreateOrderCommandValidator()
        {
                RuleFor(x => x.Dto.CustomerId).NotEmpty().WithMessage("CustomerId is required.");
                RuleFor(x => x.Dto.OrderItems).NotEmpty().WithMessage("OrderItems are required.");
                RuleFor(x => x.Dto.OrderName).NotEmpty().WithMessage("OrderName is required.");
        }
}






