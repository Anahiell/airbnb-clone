using FluentValidation;

namespace Airbnb.UserManagement.Application.BoundedContexts.RoleManagement.Queries.GetRoleByIdQuery;

public class GetRoleByIdQueryValidator : AbstractValidator<GetRoleByIdQuery>
{
    public GetRoleByIdQueryValidator()
    {
        RuleFor(q => q.Id)
            .GreaterThan(0).WithMessage("Идентификатор должен быть больше 0");
    }
}