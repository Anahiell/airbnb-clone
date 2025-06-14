using FluentValidation;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserPermissionManagement.Commands.DeleteUserPermissionCommand;


public class DeleteUserPermissionCommandValidator : AbstractValidator<DeleteUserPermissionCommand>
{
    public DeleteUserPermissionCommandValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThan(0).WithMessage("ID должен быть положительным");
    }
}