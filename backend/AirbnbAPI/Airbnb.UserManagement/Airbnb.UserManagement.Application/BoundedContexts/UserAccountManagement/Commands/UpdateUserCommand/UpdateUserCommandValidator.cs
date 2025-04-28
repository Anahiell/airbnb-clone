using FluentValidation;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Commands.UpdateUserCommand;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThan(0).WithMessage("ID должен быть положительным числом");

        RuleFor(c => c.FullName)
            .NotEmpty().WithMessage("Полное имя не может быть пустым");

        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("Email не может быть пустым")
            .EmailAddress().WithMessage("Email должен быть в правильном формате");

        RuleFor(c => c.Role)
            .IsInEnum().WithMessage("Роль должна быть валидной");

        RuleFor(c => c.DateOfBirth)
            .LessThan(DateTime.Now).WithMessage("Дата рождения не может быть в будущем");
    }
}