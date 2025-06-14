using FluentValidation;

namespace Airbnb.VerificationManagement.Application.BoundedContexts.DocumentManagement.Commands.UpdateDocumentCommand;

public class UpdateDocumentCommandValidator : AbstractValidator<UpdateDocumentCommand>
{
    public UpdateDocumentCommandValidator()
    {
        RuleFor(x => x.DocumentId).GreaterThan(0);
        RuleFor(x => x.Data).NotNull().Must(d => d.Count > 0).WithMessage("Document data is required");
    }
}