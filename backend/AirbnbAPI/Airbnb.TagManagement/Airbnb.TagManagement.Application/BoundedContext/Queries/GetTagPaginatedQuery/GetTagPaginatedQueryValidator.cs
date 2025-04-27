using FluentValidation;

namespace Airbnb.TagsManagement.Application.BoundedContext.Queries.GetTagPaginatedQuery;

public class GetTagPaginatedQueryValidator : AbstractValidator<GetTagPaginatedQuery>
{
    public GetTagPaginatedQueryValidator()
    {
        RuleFor(q => q.Page)
            .GreaterThan(0).WithMessage("Номер страницы должен быть больше 0");

        RuleFor(q => q.PageSize)
            .GreaterThan(0).WithMessage("Размер страницы должен быть больше 0")
            .LessThanOrEqualTo(100).WithMessage("Размер страницы не может превышать 100");

        RuleFor(q => q.SortOrder)
            .IsInEnum().WithMessage("Некорректный тип сортировки");
    }
}