using FluentValidation;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductFeatureManagement.Commands.DeleteFeatureCommand;

public class DeleteFeatureCommandValidator : AbstractValidator<DeleteFeatureCommand>
{
    public DeleteFeatureCommandValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThan(0).WithMessage("Id должен быть положительным числом");
    }
}