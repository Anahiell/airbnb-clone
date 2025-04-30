using FluentValidation;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Commands.LoginCommand;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("Email не может быть пустым")
            .EmailAddress().WithMessage("Некорректный формат email");

        RuleFor(c => c.Password)
            .NotEmpty().WithMessage("Пароль не может быть пустым")
            .MinimumLength(6).WithMessage("Пароль должен быть не менее 6 символов")
            .MaximumLength(20).WithMessage("Пароль не может быть длиннее 20 символов");
    }
}