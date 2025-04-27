using FluentValidation;

namespace Airbnb.PictureManagement.Application.BoundedContext.Commands;

public class CreatePictureCommandValidator : AbstractValidator<CreatePictureCommand>
{
    public CreatePictureCommandValidator()
    {
        RuleFor(c => c.Url)
            .NotEmpty().WithMessage("URL не может быть пустым")
            .MaximumLength(500).WithMessage("URL не может быть длиннее 500 символов");

        RuleFor(c => c.UserId)
            .GreaterThan(0).WithMessage("UserId должен быть положительным числом");
    }
}