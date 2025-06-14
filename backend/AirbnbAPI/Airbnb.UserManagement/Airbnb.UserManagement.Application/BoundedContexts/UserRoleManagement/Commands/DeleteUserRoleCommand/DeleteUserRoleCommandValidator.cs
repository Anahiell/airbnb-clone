using FluentValidation;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserRoleManagement.Commands.DeleteUserRoleCommand;

public class DeleteUserRoleCommandValidator : AbstractValidator<DeleteUserRoleCommand>
{
    public DeleteUserRoleCommandValidator()
    {
        RuleFor(c => c.UserId)
            .GreaterThan(0).WithMessage("ID пользователя должен быть положительным");

        RuleFor(c => c.RoleId)
            .GreaterThan(0).WithMessage("ID роли должен быть положительным");
    }
}