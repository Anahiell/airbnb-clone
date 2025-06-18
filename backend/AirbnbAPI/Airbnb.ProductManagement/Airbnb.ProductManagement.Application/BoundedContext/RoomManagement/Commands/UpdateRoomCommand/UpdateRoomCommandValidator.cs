using FluentValidation;

namespace Airbnb.ProductManagement.Application.BoundedContext.RoomManagement.Commands.UpdateRoomCommand;

public class UpdateRoomCommandValidator : AbstractValidator<UpdateRoomCommand>
{
    public UpdateRoomCommandValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThan(0).WithMessage("Id должен быть положительным числом");

        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Название обязательно")
            .MaximumLength(100).WithMessage("Название не должно превышать 100 символов");

        RuleFor(c => c.Capacity)
            .GreaterThan(0).WithMessage("Вместимость должна быть положительной");
    }
}