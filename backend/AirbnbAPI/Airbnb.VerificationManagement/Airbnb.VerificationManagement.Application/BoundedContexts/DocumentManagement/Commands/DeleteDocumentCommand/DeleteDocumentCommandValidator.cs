using FluentValidation;

namespace Airbnb.VerificationManagement.Application.BoundedContexts.DocumentManagement.Commands.DeleteDocumentCommand;

public class DeleteDocumentCommandValidator : AbstractValidator<DeleteDocumentCommand>
{
    public DeleteDocumentCommandValidator()
    {
        RuleFor(x => x.DocumentId).GreaterThan(0);
    }
}