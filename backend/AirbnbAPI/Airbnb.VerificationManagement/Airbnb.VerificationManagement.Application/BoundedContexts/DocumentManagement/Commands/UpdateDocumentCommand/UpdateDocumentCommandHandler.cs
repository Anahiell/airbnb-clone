namespace Airbnb.VerificationManagement.Application.BoundedContexts.DocumentManagement.Commands.UpdateDocumentCommand;

public class UpdateDocumentCommandHandler : ICommandHandler<UpdateDocumentCommand, Result>
{
    private readonly IRepository<DomainDocument> _repository;
    private readonly IDocumentTypeRegistry _documentTypeRegistry;
    private readonly IPublisher _publisher;

    public UpdateDocumentCommandHandler(
        IRepository<DomainDocument> repository,
        IDocumentTypeRegistry documentTypeRegistry,
        IPublisher publisher)
    {
        _repository = repository;
        _documentTypeRegistry = documentTypeRegistry;
        _publisher = publisher;
    }

    public async Task<Result> Handle(UpdateDocumentCommand request, CancellationToken ct)
    {
        var document = await _repository.GetByIdAsync(request.DocumentId, ct);
        if (document is null)
            return Result.Failure("Документ не найден");

        var documentType = _documentTypeRegistry.ResolveType(document.Type);
        if (documentType is null)
            return Result.Failure($"Тип документа не зарегистрирован: {document.Type}");

        document.Update(request.Data, documentType);

        await _repository.UpdateAsync(document, ct);
        await _publisher.PublishAll(document.DequeueEvents(), ct);

        return Result.Success();
    }
}