using FluentValidation;

namespace Airbnb.ReviewManagement.Application.BoundedContext.Commands.UpdateReviewCommand;

public class UpdateReviewCommandValidator : AbstractValidator<UpdateReviewCommand>
{
    public UpdateReviewCommandValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThan(0).WithMessage("Id должен быть положительным числом");

        RuleFor(c => c.Comment)
            .NotEmpty().WithMessage("Комментарий не может быть пустым")
            .MaximumLength(1000).WithMessage("Комментарий не может быть длиннее 1000 символов");

        RuleFor(c => c.Rating)
            .InclusiveBetween(1, 5).WithMessage("Рейтинг должен быть между 1 и 5");
    }
}