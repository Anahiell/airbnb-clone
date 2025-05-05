using FluentValidation;

namespace Airbnb.OrderManagement.Application.BoundedContext.Queries.GetOrdersByProductId;

public class GetOrdersByProductIdQueryValidator : AbstractValidator<GetOrdersByProductIdQuery>
{
    public GetOrdersByProductIdQueryValidator()
    {
        RuleFor(q => q.ProductId)
            .GreaterThan(0).WithMessage("ProductId должен быть больше 0");
    }
}