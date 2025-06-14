using FluentValidation;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserPermissionManagement.Commands.CreateUserPermissionCommand;

public class CreateUserPermissionCommandValidator : AbstractValidator<CreateUserPermissionCommand>
{
    public CreateUserPermissionCommandValidator()
    {
        RuleFor(c => c.UserId)
            .GreaterThan(0).WithMessage("ID пользователя должен быть положительным");

        RuleFor(c => c.PermissionId)
            .GreaterThan(0).WithMessage("ID разрешения должен быть положительным");
    }
}