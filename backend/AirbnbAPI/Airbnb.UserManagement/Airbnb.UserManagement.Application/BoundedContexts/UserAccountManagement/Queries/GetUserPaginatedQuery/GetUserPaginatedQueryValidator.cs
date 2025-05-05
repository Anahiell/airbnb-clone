using FluentValidation;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Queries;

public class GetUserPaginatedQueryValidator : AbstractValidator<GetUserPaginatedQuery>
{
    public GetUserPaginatedQueryValidator()
    {
        RuleFor(q => q.Page)
            .GreaterThan(0).WithMessage("Номер страницы должен быть больше 0");

        RuleFor(q => q.PageSize)
            .GreaterThan(0).WithMessage("Размер страницы должен быть больше 0")
            .LessThanOrEqualTo(100).WithMessage("Размер страницы не может превышать 100");

        RuleFor(q => q.Role)
            .GreaterThan(0)
            .When(q => q.Role.HasValue)
            .WithMessage("Роль пользователя должна быть указана и больше 0");

        RuleFor(q => q.CreatedAfter)
            .LessThan(q => q.CreatedBefore ?? DateTime.MaxValue)
            .When(q => q.CreatedAfter.HasValue && q.CreatedBefore.HasValue)
            .WithMessage("Дата начала должна быть раньше даты окончания");
    }
}