using FluentValidation;

namespace Airbnb.UserManagement.Application.BoundedContexts.PermissionManagement.Commands.UpdatePermissionCommand;

public class UpdatePermissionCommandValidator : AbstractValidator<UpdatePermissionCommand>
{
    public UpdatePermissionCommandValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThan(0).WithMessage("ID должен быть положительным");

        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Название разрешения не может быть пустым")
            .MinimumLength(2).WithMessage("Название должно быть больше 2 символов")
            .MaximumLength(100).WithMessage("Название не может быть длиннее 100 символов");
    }
}