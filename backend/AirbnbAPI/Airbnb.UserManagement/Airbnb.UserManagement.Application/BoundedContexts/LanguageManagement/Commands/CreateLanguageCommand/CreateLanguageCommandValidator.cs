using FluentValidation;

namespace Airbnb.UserManagement.Application.BoundedContexts.LanguageManagement.Commands.CreateLanguageCommand;

public class CreateLanguageCommandValidator : AbstractValidator<CreateLanguageCommand>
{
    public CreateLanguageCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Название языка не может быть пустым")
            .MinimumLength(2).WithMessage("Название должно быть больше 2 символов")
            .MaximumLength(50).WithMessage("Название не может быть длиннее 50 символов");
    }
}