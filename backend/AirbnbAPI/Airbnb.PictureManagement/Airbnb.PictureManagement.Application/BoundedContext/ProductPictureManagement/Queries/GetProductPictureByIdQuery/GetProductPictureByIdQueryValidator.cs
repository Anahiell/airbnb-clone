using FluentValidation;

namespace Airbnb.PictureManagement.Application.BoundedContext.ProductPictureManagement.Queries.GetProductPictureByIdQuery;

public class GetProductPictureByIdQueryValidator : AbstractValidator<GetProductPictureByIdQuery>
{
    public GetProductPictureByIdQueryValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThan(0).WithMessage("ProductId должен быть положительным числом");
    }
}