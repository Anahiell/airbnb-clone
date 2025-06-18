using FluentValidation;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductFacilityManagement.Queries.GetFacilityPaginatedQuery;

public class GetFeatureByIdQueryValidator : AbstractValidator<GetFeatureByIdQuery>
{
    public GetFeatureByIdQueryValidator()
    {
        RuleFor(x => x.FeatureId)
            .GreaterThan(0).WithMessage("FeatureId должен быть положительным числом");
    }
}