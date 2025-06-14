using FluentValidation;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductFacilityManagement.Commands.UpdateFacilityCommand;

public class UpdateFacilityCommandValidator : AbstractValidator<UpdateFacilityCommand>
{
    public UpdateFacilityCommandValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThan(0).WithMessage("Id должен быть положительным числом");

        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Название обязательно")
            .MaximumLength(100).WithMessage("Название не должно превышать 100 символов");
    }
}