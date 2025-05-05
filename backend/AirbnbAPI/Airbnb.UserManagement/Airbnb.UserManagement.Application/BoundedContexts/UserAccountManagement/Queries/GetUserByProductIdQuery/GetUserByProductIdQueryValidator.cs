using FluentValidation;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Queries.GetUserByProductIdQuery;

public class GetUserByProductIdQueryValidator : AbstractValidator<GetUserByProductIdQuery>
{
    public GetUserByProductIdQueryValidator()
    {
        RuleFor(q => q.ProductId)
            .GreaterThan(0).WithMessage("ProductId должен быть больше 0");
    }
}