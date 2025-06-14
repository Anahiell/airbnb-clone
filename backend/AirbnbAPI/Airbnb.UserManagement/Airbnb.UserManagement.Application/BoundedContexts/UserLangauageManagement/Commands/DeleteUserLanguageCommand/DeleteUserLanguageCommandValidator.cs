using FluentValidation;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserLangauageManagement.Commands.DeleteUserLanguageCommand;

public class DeleteUserLanguageCommandValidator : AbstractValidator<DeleteUserLanguageCommand>
{
    public DeleteUserLanguageCommandValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThan(0).WithMessage("Id должен быть положительным числом");
    }
}