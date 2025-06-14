using FluentValidation;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserLangauageManagement.Commands.UpdateUserLanguageCommand;

public class UpdateUserLanguageCommandValidator : AbstractValidator<UpdateUserLanguageCommand>
{
    public UpdateUserLanguageCommandValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThan(0).WithMessage("Id должен быть положительным числом");
    }
}