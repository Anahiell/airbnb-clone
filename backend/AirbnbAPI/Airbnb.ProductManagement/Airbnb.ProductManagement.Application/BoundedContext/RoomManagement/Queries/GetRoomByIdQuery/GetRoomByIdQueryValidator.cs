using FluentValidation;

namespace Airbnb.ProductManagement.Application.BoundedContext.RoomManagement.Queries.GetRoomByIdQuery;

public class GetRoomByIdQueryValidator : AbstractValidator<GetRoomByIdQuery>
{
    public GetRoomByIdQueryValidator()
    {
        RuleFor(x => x.RoomId)
            .GreaterThan(0).WithMessage("RoomId должен быть положительным числом");
    }
}