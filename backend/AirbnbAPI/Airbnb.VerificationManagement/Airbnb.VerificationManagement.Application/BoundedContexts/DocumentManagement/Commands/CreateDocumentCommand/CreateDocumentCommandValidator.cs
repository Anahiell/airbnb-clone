using FluentValidation;

namespace Airbnb.VerificationManagement.Application.BoundedContexts.DocumentManagement.Commands.CreateDocumentCommand;

public class CreateDocumentCommandValidator : AbstractValidator<CreateDocumentCommand>
{
    public CreateDocumentCommandValidator()
    {
        RuleFor(x => x.UserId).GreaterThan(0);
        RuleFor(x => x.DocumentType).NotEmpty();
    }
}