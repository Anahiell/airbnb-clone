using FluentValidation;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserRoleManagement.Commands.CreateUserRoleCommand;

public class CreateUserRoleCommandValidator : AbstractValidator<CreateUserRoleCommand>
{
    public CreateUserRoleCommandValidator()
    {
        RuleFor(c => c.UserId)
            .GreaterThan(0).WithMessage("ID пользователя должен быть положительным");

        RuleFor(c => c.RoleId)
            .GreaterThan(0).WithMessage("ID роли должен быть положительным");
    }
}
