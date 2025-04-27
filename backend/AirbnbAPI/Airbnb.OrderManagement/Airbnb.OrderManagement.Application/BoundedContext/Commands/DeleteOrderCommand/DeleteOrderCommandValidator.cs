using FluentValidation;

namespace Airbnb.OrderManagement.Application.BoundedContext.Commands.DeleteOrderCommand;

public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderCommandValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThan(0).WithMessage("ID должен быть положительным числом");
    }
}