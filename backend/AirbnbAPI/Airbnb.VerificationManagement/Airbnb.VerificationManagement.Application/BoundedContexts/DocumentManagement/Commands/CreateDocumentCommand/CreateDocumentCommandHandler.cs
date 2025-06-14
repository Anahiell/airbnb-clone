using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.PictureManagement.Application.BoundedContext.FileService;
using Airbnb.SharedKernel.Repositories;
using Airbnb.VerificationManagement.Application.BoundedContexts.DocumentManagement.Services.DocumentDispatcher;
using Airbnb.VerificationManagement.Domain.BoundedContexts.DocumentManagement.DomainDocumentManagement.Interfaces;
using Airbnb.VerificationManagement.Domain.BoundedContexts.DocumentManagement.DriverLicenseManagement.Events;
using Airbnb.VerificationManagement.Domain.BoundedContexts.DocumentManagement.DriverLicenseManagement.ValueObjects.DocumentType;
using Airbnb.VerificationManagement.Domain.BoundedContexts.DocumentManagement.PassportManagement.Aggregates;
using Airbnb.VerificationManagement.Domain.BoundedContexts.VerificationManagement.Aggregates;
using MediatR;

namespace Airbnb.VerificationManagement.Application.BoundedContexts.DocumentManagement.Commands.CreateDocumentCommand;

public class CreateDocumentCommandHandler : ICommandHandler<CreateDocumentCommand, Result<int>>
{
    private readonly IDomainDocumentRepository _repository;
    private readonly IMediator _mediator;
    private readonly IFileService _fileService;
    private readonly IDocumentFactory _dispatcher;

    public CreateDocumentCommandHandler(IDomainDocumentRepository repository, IMediator mediator, IFileService fileService, 
        IDocumentFactory dispatcher)
    {
        _repository = repository;
        _mediator = mediator;
        _fileService = fileService;
        _dispatcher = dispatcher;
    }

    public async Task<Result<int>> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
    {
        var filePath = await _fileService.SaveAsync(request.File, request.DocumentType.ToString(), cancellationToken);

        var documentType = new DocumentType(request.DocumentType);

        var document = _dispatcher.Create<PassportDocument>(request.UserId, filePath);
        
        var documentId = await _repository.AddAsync(document, cancellationToken);

        await _mediator.Publish(new DocumentCreatedEvent(documentId, document.UserId, documentType, document.FilePath) , cancellationToken);

        return Result<int>.Success(document.Id);
    }
}