using FluentValidation;

namespace Airbnb.PictureManagement.Application.BoundedContext.UserPictureManagement.Queries.GetUserPictureByIdQuery;

public class GetUserPictureByIdQueryValidator : AbstractValidator<GetUserPictureByIdQuery>
{
    public GetUserPictureByIdQueryValidator()
    {
        RuleFor(c => c.UserId)
            .GreaterThan(0).WithMessage("UserId должен быть положительным числом");
    }
}