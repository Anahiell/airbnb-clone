using FluentValidation;

namespace Airbnb.ProductManagement.Application.BoundedContext.Queries.GetProductRatingByIdQuery;

public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
{
    public GetProductByIdQueryValidator()
    {
        RuleFor(q => q.ProductId)
            .GreaterThan(0).WithMessage("Идентификатор продукта должен быть больше 0");
    }
}