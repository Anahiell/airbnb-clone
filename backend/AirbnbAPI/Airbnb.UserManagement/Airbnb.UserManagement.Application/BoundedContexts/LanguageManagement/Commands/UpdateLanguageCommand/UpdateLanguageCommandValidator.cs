using FluentValidation;

namespace Airbnb.UserManagement.Application.BoundedContexts.LanguageManagement.Commands.UpdateLanguageCommand;

public class UpdateLanguageCommandValidator : AbstractValidator<UpdateLanguageCommand>
{
    public UpdateLanguageCommandValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThan(0).WithMessage("ID должен быть положительным");

        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Название языка не может быть пустым")
            .MinimumLength(2).WithMessage("Название должно быть больше 2 символов")
            .MaximumLength(50).WithMessage("Название не может быть длиннее 50 символов");
    }
}