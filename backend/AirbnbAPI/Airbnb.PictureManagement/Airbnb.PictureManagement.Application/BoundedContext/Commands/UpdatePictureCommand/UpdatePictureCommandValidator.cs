using FluentValidation;

namespace Airbnb.PictureManagement.Application.BoundedContext.Commands.UpdatePictureCommand;

public class UpdatePictureCommandValidator : AbstractValidator<UpdatePictureCommand>
{
    public UpdatePictureCommandValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThan(0).WithMessage("Id должен быть положительным числом");

        RuleFor(c => c.Url)
            .NotEmpty().WithMessage("URL не может быть пустым")
            .MaximumLength(500).WithMessage("URL не может быть длиннее 500 символов");
    }
}