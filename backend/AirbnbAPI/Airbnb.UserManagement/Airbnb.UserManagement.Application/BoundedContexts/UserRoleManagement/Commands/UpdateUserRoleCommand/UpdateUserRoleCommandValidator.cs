using FluentValidation;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserRoleManagement.Commands.UpdateUserRoleCommand;

public class UpdateUserRoleCommandValidator : AbstractValidator<UpdateUserRoleCommand>
{
    public UpdateUserRoleCommandValidator()
    {
        RuleFor(c => c.UserId)
            .GreaterThan(0).WithMessage("ID пользователя должен быть положительным");

        RuleFor(c => c.NewRoleId)
            .GreaterThan(0).WithMessage("ID роли должен быть положительным");
        
        RuleFor(c => c.OldRoleId)
            .GreaterThan(0).WithMessage("ID роли должен быть положительным");
        
        RuleFor(c => c.OldRoleId).NotEqual(c => c.NewRoleId).WithMessage("Новая роль не должна совпадать со старой");
    }
}