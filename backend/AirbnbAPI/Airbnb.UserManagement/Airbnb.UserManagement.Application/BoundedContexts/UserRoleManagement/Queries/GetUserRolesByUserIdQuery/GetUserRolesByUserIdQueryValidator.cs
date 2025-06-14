using FluentValidation;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserRoleManagement.Queries.GetUserRolesByUserIdQuery;

public class GetUserRolesByUserIdQueryValidator : AbstractValidator<GetUserRolesByUserIdQuery>
{
    public GetUserRolesByUserIdQueryValidator()
    {
        RuleFor(q => q.UserId)
            .GreaterThan(0).WithMessage("ID пользователя должен быть больше 0");
    }
}