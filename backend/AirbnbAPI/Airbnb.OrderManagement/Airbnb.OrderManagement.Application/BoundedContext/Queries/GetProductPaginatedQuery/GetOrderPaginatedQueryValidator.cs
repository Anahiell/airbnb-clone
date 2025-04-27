using FluentValidation;

namespace Airbnb.OrderManagement.Application.BoundedContext.Queries.GetProductPaginatedQuery;

public class GetOrderPaginatedQueryValidator : AbstractValidator<GetOrderPaginatedQuery>
{
    public GetOrderPaginatedQueryValidator()
    {
        RuleFor(q => q.Page)
            .GreaterThan(0).WithMessage("Номер страницы должен быть больше 0");

        RuleFor(q => q.PageSize)
            .GreaterThan(0).WithMessage("Размер страницы должен быть больше 0")
            .LessThanOrEqualTo(100).WithMessage("Размер страницы не может превышать 100");

        RuleFor(q => q.SortOrder)
            .IsInEnum().WithMessage("Некорректный тип сортировки");

        RuleFor(q => q.DateStartAfter)
            .LessThan(q => q.DateEndBefore ?? DateTime.MaxValue)
            .When(q => q.DateStartAfter.HasValue && q.DateEndBefore.HasValue)
            .WithMessage("Дата начала аренды должна быть раньше даты окончания аренды");
    }
}