using FluentValidation;

namespace Airbnb.UserManagement.Application.BoundedContexts.RoleManagement.Commands.DeleteRoleCommand;

public class DeleteRoleCommandValidator : AbstractValidator<DeleteRoleCommand>
{
    public DeleteRoleCommandValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThan(0).WithMessage("ID должен быть положительным");
    }
}