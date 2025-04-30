using FluentValidation;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Commands.RegisterUserCommand;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(c => c.FullName)
            .NotEmpty().WithMessage("Полное имя не может быть пустым")
            .MinimumLength(3).WithMessage("Полное имя должно быть не менее 3 символов")
            .MaximumLength(100).WithMessage("Полное имя не может быть длиннее 100 символов");

        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("Email не может быть пустым")
            .EmailAddress().WithMessage("Некорректный формат email");

        RuleFor(c => c.Password)
            .NotEmpty().WithMessage("Пароль не может быть пустым")
            .MinimumLength(6).WithMessage("Пароль должен быть не менее 6 символов")
            .MaximumLength(20).WithMessage("Пароль не может быть длиннее 20 символов");

        RuleFor(c => c.DateOfBirth)
            .NotEmpty().WithMessage("Дата рождения не может быть пустой")
            .Must(BeAValidAge).WithMessage("Пользователь должен быть старше 18 лет");

        RuleFor(c => c.Roles)
            .NotEmpty().WithMessage("Роли не могут быть пустыми");
    }

    private bool BeAValidAge(DateTime dateOfBirth)
    {
        return DateTime.Now.Year - dateOfBirth.Year >= 18;
    }
}