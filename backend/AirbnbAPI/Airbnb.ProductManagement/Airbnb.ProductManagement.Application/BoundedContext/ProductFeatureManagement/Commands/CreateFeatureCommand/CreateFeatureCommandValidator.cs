using FluentValidation;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductFeatureManagement.Commands.CreateFeatureCommand;

public class CreateFeatureCommandValidator : AbstractValidator<CreateFeatureCommand>
{
    public CreateFeatureCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Название обязательно")
            .MaximumLength(100).WithMessage("Название не должно превышать 100 символов");

        RuleFor(c => c.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Цена должна быть неотрицательной");
    }
}