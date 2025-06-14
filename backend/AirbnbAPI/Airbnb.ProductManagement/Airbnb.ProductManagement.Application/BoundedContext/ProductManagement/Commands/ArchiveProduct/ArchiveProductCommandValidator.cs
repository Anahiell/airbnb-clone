using FluentValidation;

namespace Airbnb.ProductManagement.Application.BoundedContext.Commands.ArchiveProduct;

public class ArchiveProductCommandValidator : AbstractValidator<ArchiveProductCommand>
{
    public ArchiveProductCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThan(0).WithMessage("ID продукта должен быть больше 0");
    }
}