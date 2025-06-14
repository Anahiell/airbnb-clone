using FluentValidation;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserLangauageManagement.Commands.CreateUserLanguageCommand;

public class CreateUserLanguageCommandValidator : AbstractValidator<CreateUserLanguageCommand>
{
    public CreateUserLanguageCommandValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("ID пользователя должен быть больше 0");

        RuleFor(x => x.LanguageId)
            .GreaterThan(0).WithMessage("ID языка должен быть больше 0");
    }
}