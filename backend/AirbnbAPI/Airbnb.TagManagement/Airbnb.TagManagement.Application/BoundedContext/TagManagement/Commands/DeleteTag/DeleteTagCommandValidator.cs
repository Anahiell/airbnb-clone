using FluentValidation;

namespace Airbnb.TagsManagement.Application.BoundedContext.Commands.DeleteTag;


public class DeleteTagCommandValidator : AbstractValidator<DeleteTagCommand>
{
    public DeleteTagCommandValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThan(0).WithMessage("Id должен быть положительным числом");
    }
}