using FluentValidation;

namespace Airbnb.ReviewManagement.Application.BoundedContext.Queries;

public class GetReviewPaginatedQueryValidator : AbstractValidator<GetReviewPaginatedQuery>
{
    public GetReviewPaginatedQueryValidator()
    {
        RuleFor(q => q.Page)
            .GreaterThan(0).WithMessage("Номер страницы должен быть больше 0");

        RuleFor(q => q.PageSize)
            .GreaterThan(0).WithMessage("Размер страницы должен быть больше 0")
            .LessThanOrEqualTo(100).WithMessage("Размер страницы не может превышать 100");

        RuleFor(q => q.MinRating)
            .InclusiveBetween(0, 5)
            .When(q => q.MinRating.HasValue)
            .WithMessage("Минимальный рейтинг должен быть от 0 до 5");

        RuleFor(q => q.MaxRating)
            .InclusiveBetween(0, 5)
            .When(q => q.MaxRating.HasValue)
            .WithMessage("Максимальный рейтинг должен быть от 0 до 5");

        RuleFor(q => q.SortOrder)
            .IsInEnum().WithMessage("Некорректный тип сортировки");

        RuleFor(q => q.CreatedAfter)
            .LessThan(q => q.CreatedBefore ?? DateTime.MaxValue)
            .When(q => q.CreatedAfter.HasValue && q.CreatedBefore.HasValue)
            .WithMessage("Дата начала должна быть раньше даты окончания");
    }
}