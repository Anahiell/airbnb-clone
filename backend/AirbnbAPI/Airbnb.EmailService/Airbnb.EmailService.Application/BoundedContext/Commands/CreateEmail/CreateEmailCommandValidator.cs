using FluentValidation;

namespace Airbnb.EmailService.Application.BoundedContext.Commands.CreateEmail;

public class CreateEmailCommandValidator : AbstractValidator<CreateEmailCommand>
{
    public CreateEmailCommandValidator()
    {
        RuleFor(c => c.Subject)
            .NotEmpty().WithMessage("Тема email не может быть пустой")
            .MaximumLength(100).WithMessage("Тема не может быть длиннее 100 символов");

        RuleFor(c => c.Body)
            .NotEmpty().WithMessage("Тело email не может быть пустым")
            .MaximumLength(1000).WithMessage("Тело не может быть длиннее 1000 символов");

        RuleFor(c => c.Recipient)
            .NotEmpty().WithMessage("Получатель email не может быть пустым")
            .EmailAddress().WithMessage("Неверный формат email");
    }
}