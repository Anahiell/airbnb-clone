using FluentValidation;

namespace Airbnb.TagsManagement.Application.BoundedContext.Commands.CreateTag;

public class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
{
    public CreateTagCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Название тега не может быть пустым")
            .MinimumLength(2).WithMessage("Название должно быть больше 2 символов")
            .MaximumLength(50).WithMessage("Название не может быть длиннее 50 символов");
    }
}