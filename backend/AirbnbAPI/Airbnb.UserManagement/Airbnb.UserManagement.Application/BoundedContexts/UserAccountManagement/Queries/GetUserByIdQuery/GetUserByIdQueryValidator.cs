using FluentValidation;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Queries.GetUserByIdQuery;

public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(q => q.Id)
            .NotEmpty().WithMessage("Id пользователя не может быть пустым");
    }
}