using FluentValidation;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductFacilityManagement.Commands.DeleteFacilityCommand;

public class DeleteFacilityCommandValidator : AbstractValidator<DeleteFacilityCommand>
{
    public DeleteFacilityCommandValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThan(0).WithMessage("Id должен быть положительным числом");
    }
}