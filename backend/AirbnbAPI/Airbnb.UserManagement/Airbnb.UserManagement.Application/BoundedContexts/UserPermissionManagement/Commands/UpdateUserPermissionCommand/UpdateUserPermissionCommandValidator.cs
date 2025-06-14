using FluentValidation;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserPermissionManagement.Commands.UpdateUserPermissionCommand;

public class UpdateUserPermissionCommandValidator : AbstractValidator<UpdateUserPermissionCommand>
{
    public UpdateUserPermissionCommandValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThan(0).WithMessage("ID должен быть положительным");

        RuleFor(c => c.PermissionId)
            .GreaterThan(0).WithMessage("ID разрешения должен быть положительным");
    }
}