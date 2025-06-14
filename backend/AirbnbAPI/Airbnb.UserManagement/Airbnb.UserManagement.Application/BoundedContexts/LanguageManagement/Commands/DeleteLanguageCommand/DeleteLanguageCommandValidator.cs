using FluentValidation;

namespace Airbnb.UserManagement.Application.BoundedContexts.LanguageManagement.Commands.DeleteLanguageCommand;

public class DeleteLanguageCommandValidator : AbstractValidator<DeleteLanguageCommand>
{
    public DeleteLanguageCommandValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThan(0).WithMessage("ID должен быть положительным");
    }
}