using FluentValidation;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductFacilityManagement.Queries.GetFacilityByIdQuery;

public class GetFacilityByIdQueryValidator : AbstractValidator<GetFacilityByIdQuery>
{
    public GetFacilityByIdQueryValidator()
    {
        RuleFor(x => x.FacilityId)
            .GreaterThan(0).WithMessage("FacilityId должен быть положительным числом");
    }
}