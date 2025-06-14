using FluentValidation;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductFacilityManagement.Commands.CreateFacilityCommand;

public class CreateFacilityCommandValidator : AbstractValidator<CreateFacilityCommand>
{
    public CreateFacilityCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Название обязательно")
            .MaximumLength(100).WithMessage("Название не должно превышать 100 символов");
    }
}