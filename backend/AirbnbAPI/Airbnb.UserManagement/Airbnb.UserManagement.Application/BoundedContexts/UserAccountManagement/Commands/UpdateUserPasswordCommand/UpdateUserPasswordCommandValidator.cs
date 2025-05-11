using FluentValidation;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Commands.UpdateUserPasswordCommand;

public class UpdateUserPasswordCommandValidator : AbstractValidator<UpdateUserPasswordCommand>
{
    public UpdateUserPasswordCommandValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThan(0).WithMessage("ID должен быть положительным числом");

        RuleFor(c => c.CurrentPassword)
            .NotEmpty().WithMessage("Текущий пароль обязателен");

        RuleFor(c => c.NewPassword)
            .NotEmpty().WithMessage("Новый пароль обязателен")
            .MinimumLength(6).WithMessage("Пароль должен содержать не менее 6 символов");
    }
}