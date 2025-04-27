using FluentValidation;

namespace Airbnb.ReviewManagement.Application.BoundedContext.Commands.DeleteReviewCommand;

public class DeleteReviewCommandValidator : AbstractValidator<DeleteReviewCommand>
{
    public DeleteReviewCommandValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThan(0).WithMessage("Id должен быть положительным числом");
    }
}