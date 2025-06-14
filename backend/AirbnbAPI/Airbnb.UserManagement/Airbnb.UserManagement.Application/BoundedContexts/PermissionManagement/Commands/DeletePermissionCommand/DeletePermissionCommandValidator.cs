using FluentValidation;

namespace Airbnb.UserManagement.Application.BoundedContexts.PermissionManagement.Commands.DeletePermissionCommand;

public class DeletePermissionCommandValidator : AbstractValidator<DeletePermissionCommand>
{
    public DeletePermissionCommandValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThan(0).WithMessage("ID должен быть положительным");
    }
}