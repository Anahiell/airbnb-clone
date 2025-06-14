using FluentValidation;

namespace Airbnb.UserManagement.Application.BoundedContexts.LanguageManagement.Queries;

public class GetLanguageByIdQueryValidator : AbstractValidator<GetLanguageByIdQuery>
{
    public GetLanguageByIdQueryValidator()
    {
        RuleFor(q => q.Id)
            .GreaterThan(0).WithMessage("Идентификатор должен быть больше 0");
    }
}