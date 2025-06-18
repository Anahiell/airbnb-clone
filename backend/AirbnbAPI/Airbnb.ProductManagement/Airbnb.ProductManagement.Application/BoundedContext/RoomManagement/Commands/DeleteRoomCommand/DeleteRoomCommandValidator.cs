using FluentValidation;

namespace Airbnb.ProductManagement.Application.BoundedContext.RoomManagement.Commands.DeleteRoomCommand;

public class DeleteRoomCommandValidator : AbstractValidator<DeleteRoomCommand>
{
    public DeleteRoomCommandValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThan(0).WithMessage("Id должен быть положительным числом");
    }
}