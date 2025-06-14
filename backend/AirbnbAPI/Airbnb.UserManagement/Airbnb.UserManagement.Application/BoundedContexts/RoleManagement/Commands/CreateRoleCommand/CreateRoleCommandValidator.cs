using FluentValidation;

namespace Airbnb.UserManagement.Application.BoundedContexts.RoleManagement.Commands.CreateRoleCommand;

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Название роли не может быть пустым")
            .MinimumLength(2).WithMessage("Название должно быть больше 2 символов")
            .MaximumLength(50).WithMessage("Название не может быть длиннее 50 символов");
    }
}