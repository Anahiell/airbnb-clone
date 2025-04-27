using FluentValidation;

namespace Airbnb.PictureManagement.Application.BoundedContext.Commands.DeletePictureCommand;

public class DeletePictureCommandValidator : AbstractValidator<DeletePictureCommand>
{
    public DeletePictureCommandValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThan(0).WithMessage("Id должен быть положительным числом");
    }
}