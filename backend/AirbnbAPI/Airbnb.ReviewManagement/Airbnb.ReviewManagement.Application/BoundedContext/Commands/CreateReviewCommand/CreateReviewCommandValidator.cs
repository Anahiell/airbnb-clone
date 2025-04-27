using FluentValidation;

namespace Airbnb.ReviewManagement.Application.BoundedContext.Commands;

public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
{
    public CreateReviewCommandValidator()
    {
        RuleFor(c => c.Comment)
            .NotEmpty().WithMessage("Комментарий не может быть пустым")
            .MaximumLength(1000).WithMessage("Комментарий не может быть длиннее 1000 символов");

        RuleFor(c => c.Rating)
            .InclusiveBetween(1, 5).WithMessage("Рейтинг должен быть между 1 и 5");
    }
}