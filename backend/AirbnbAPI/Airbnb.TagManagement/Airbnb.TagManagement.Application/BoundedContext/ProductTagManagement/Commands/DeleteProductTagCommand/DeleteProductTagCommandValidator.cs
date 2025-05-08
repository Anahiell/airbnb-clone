using FluentValidation;

namespace Airbnb.TagsManagement.Application.BoundedContext.ProductTagManagement.Commands.DeleteProductTagCommand;

public class DeleteProductTagCommandValidator : AbstractValidator<DeleteProductTagCommand>
{
    public DeleteProductTagCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}