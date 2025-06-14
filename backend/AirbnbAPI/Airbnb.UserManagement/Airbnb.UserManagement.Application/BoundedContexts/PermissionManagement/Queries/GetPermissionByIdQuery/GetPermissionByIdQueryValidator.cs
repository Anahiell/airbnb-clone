using FluentValidation;

namespace Airbnb.UserManagement.Application.BoundedContexts.PermissionManagement.Queries;

public class GetPermissionByIdQueryValidator : AbstractValidator<GetPermissionByIdQuery>
{
    public GetPermissionByIdQueryValidator()
    {
        RuleFor(q => q.Id)
            .GreaterThan(0).WithMessage("Идентификатор должен быть больше 0");
    }
}