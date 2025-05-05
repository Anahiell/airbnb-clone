using FluentValidation;

namespace Airbnb.PictureManagement.Application.BoundedContext.UserPictureManagement.Commands.DeleteUserPictureCommand;

public class DeleteUserPictureCommandValidator : AbstractValidator<DeleteUserPictureCommand>
{
    public DeleteUserPictureCommandValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThan(0).WithMessage("Id должен быть положительным числом");
    }
}