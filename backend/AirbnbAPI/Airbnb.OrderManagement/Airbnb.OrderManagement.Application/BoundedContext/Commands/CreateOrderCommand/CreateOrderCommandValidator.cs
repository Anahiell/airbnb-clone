using FluentValidation;

namespace Airbnb.OrderManagement.Application.BoundedContext.Commands.CreateOrderCommand;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(c => c.ProductId)
            .GreaterThan(0).WithMessage("ID продукта должен быть положительным числом");

        RuleFor(c => c.UserId)
            .GreaterThan(0).WithMessage("ID пользователя должен быть положительным числом");

        RuleFor(c => c.DateStart)
            .LessThan(c => c.DateEnd).WithMessage("Дата начала должна быть раньше даты окончания");

        RuleFor(c => c.DateEnd)
            .GreaterThan(c => c.DateStart).WithMessage("Дата окончания должна быть позже даты начала");
    }
}