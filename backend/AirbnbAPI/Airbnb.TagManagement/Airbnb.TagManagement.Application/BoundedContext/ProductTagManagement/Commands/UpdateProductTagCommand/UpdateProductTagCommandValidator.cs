using FluentValidation;

namespace Airbnb.TagsManagement.Application.BoundedContext.ProductTagManagement.Commands.UpdateProductTagCommand;

public class UpdateProductTagCommandValidator : AbstractValidator<UpdateProductTagCommand>
{
    public UpdateProductTagCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.NewProductId).GreaterThan(0);
        RuleFor(x => x.NewTagId).GreaterThan(0);
    }
}