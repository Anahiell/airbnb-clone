using FluentValidation;

namespace Airbnb.UserManagement.Application.BoundedContexts.RoleManagement.Commands.UpdateRoleCommand;

public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
{
    public UpdateRoleCommandValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThan(0).WithMessage("ID должен быть положительным");

        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Название роли не может быть пустым")
            .MinimumLength(2).WithMessage("Название должно быть больше 2 символов")
            .MaximumLength(50).WithMessage("Название не может быть длиннее 50 символов");
    }
}