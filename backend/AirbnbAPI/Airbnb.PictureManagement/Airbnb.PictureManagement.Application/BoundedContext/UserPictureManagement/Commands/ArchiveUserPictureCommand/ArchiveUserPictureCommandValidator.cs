using FluentValidation;

namespace Airbnb.PictureManagement.Application.BoundedContext.UserPictureManagement.Commands.ArchiveUserPictureCommand;

public class ArchiveUserPictureCommandValidator : AbstractValidator<ArchiveUserPictureCommand>
{
    public ArchiveUserPictureCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id должен быть положительным числом");
    }
}