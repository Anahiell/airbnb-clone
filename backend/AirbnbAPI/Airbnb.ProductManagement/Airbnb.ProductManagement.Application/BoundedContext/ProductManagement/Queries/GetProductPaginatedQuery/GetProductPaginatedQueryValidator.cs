using FluentValidation;

namespace Airbnb.ProductManagement.Application.BoundedContext.Queries;

public class GetProductPaginatedQueryValidator : AbstractValidator<GetProductPaginatedQuery>
{
    public GetProductPaginatedQueryValidator()
    {
        RuleFor(q => q.Page)
            .GreaterThan(0).WithMessage("Номер страницы должен быть больше 0");

        RuleFor(q => q.PageSize)
            .GreaterThan(0).WithMessage("Размер страницы должен быть больше 0")
            .LessThanOrEqualTo(100).WithMessage("Размер страницы не может превышать 100");

        RuleFor(q => q.MinPrice)
            .GreaterThanOrEqualTo(0).WithMessage("Минимальная цена не может быть отрицательной")
            .LessThan(q => q.MaxPrice ?? decimal.MaxValue)
            .WithMessage("Минимальная цена должна быть меньше максимальной");

        RuleFor(q => q.MaxPrice)
            .GreaterThanOrEqualTo(q => q.MinPrice ?? 0)
            .WithMessage("Максимальная цена должна быть больше минимальной");

        RuleFor(q => q.MinRating)
            .InclusiveBetween(0, 5)
            .WithMessage("Рейтинг должен быть от 0 до 5");

        RuleFor(q => q.MaxRating)
            .InclusiveBetween(0, 5)
            .WithMessage("Рейтинг должен быть от 0 до 5")
            .GreaterThanOrEqualTo(q => q.MinRating)
            .WithMessage("Максимальный рейтинг должен быть больше минимального");

        RuleFor(q => q.DateStart)
            .LessThan(q => q.DateEnd ?? DateTime.MaxValue)
            .WithMessage("Дата начала должна быть раньше даты окончания");

        RuleFor(q => q.DateEnd)
            .GreaterThanOrEqualTo(q => q.DateStart ?? DateTime.MinValue)
            .WithMessage("Дата окончания должна быть позже даты начала");

        RuleFor(q => q.SortOrder)
            .IsInEnum().WithMessage("Некорректный тип сортировки");
    }
}