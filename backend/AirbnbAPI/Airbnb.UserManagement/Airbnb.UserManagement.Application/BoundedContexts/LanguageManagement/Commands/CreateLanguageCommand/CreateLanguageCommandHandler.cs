using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.SharedKernel.Repositories;
using Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Aggregates;
using Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Events;
using Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Interfaces;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.LanguageManagement.Commands.CreateLanguageCommand;

public class CreateLanguageCommandHandler(
    ILanguageRepository languageRepository,
    IMediator mediator)
    : ICommandHandler<CreateLanguageCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateLanguageCommand request, CancellationToken cancellationToken)
    {
        var language = new DomainLanguage(0, request.Name);

        var result = await languageRepository.AddAsync(language, cancellationToken);

        await mediator.Publish(new LanguageCreatedEvent(language.Id, language.Name, DateTime.UtcNow), cancellationToken);

        return Result<int>.Success(result);
    }
}