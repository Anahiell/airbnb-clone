using FluentValidation;

namespace Airbnb.TagsManagement.Application.BoundedContext.ProductTagManagement.Commands.CreateProductTagCommand;

public class CreateProductTagCommandValidator : AbstractValidator<CreateProductTagCommand>
{
    public CreateProductTagCommandValidator()
    {
        RuleFor(x => x.ProductId).GreaterThan(0).WithMessage("ProductId должен быть положительным");
        RuleFor(x => x.TagId).GreaterThan(0).WithMessage("TagId должен быть положительным");
    }
}