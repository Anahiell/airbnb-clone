using FluentValidation;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserPermissionManagement.Queries.GetUserPermissionByIdQuery;

public class GetUserPermissionByIdQueryValidator : AbstractValidator<GetUserPermissionByIdQuery>
{
    public GetUserPermissionByIdQueryValidator()
    {
        RuleFor(q => q.Id)
            .GreaterThan(0).WithMessage("Идентификатор должен быть больше 0");
    }
}