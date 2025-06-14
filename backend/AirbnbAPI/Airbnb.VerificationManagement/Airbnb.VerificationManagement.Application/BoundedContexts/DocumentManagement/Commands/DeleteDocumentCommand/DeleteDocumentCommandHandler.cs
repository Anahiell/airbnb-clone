namespace Airbnb.VerificationManagement.Application.BoundedContexts.DocumentManagement.Commands.DeleteDocumentCommand;

public class DeleteDocumentCommandHandler : ICommandHandler<DeleteDocumentCommand, Result>
{
    private readonly IRepository<DomainDocument> _repository;
    private readonly IPublisher _publisher;

    public DeleteDocumentCommandHandler(IRepository<DomainDocument> repository, IPublisher publisher)
    {
        _repository = repository;
        _publisher = publisher;
    }

    public async Task<Result> Handle(DeleteDocumentCommand request, CancellationToken ct)
    {
        var document = await _repository.GetByIdAsync(request.DocumentId, ct);
        if (document is null)
            return Result.Failure("Документ не найден");

        document.MarkDeleted();

        await _repository.DeleteAsync(document, ct);
        await _publisher.PublishAll(document.DequeueEvents(), ct);

        return Result.Success();
    }
}