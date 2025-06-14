using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.SharedKernel.Repositories;
using Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Aggregates;
using Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Events;
using Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Interfaces;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.LanguageManagement.Commands.DeleteLanguageCommand;

public class DeleteLanguageCommandHandler(ILanguageRepository languageRepository, IMediator mediator)
    : ICommandHandler<DeleteLanguageCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteLanguageCommand request, CancellationToken cancellationToken)
    {
        var language = await languageRepository.GetByIdAsync(request.Id, cancellationToken);

        if (language is null)
            return Result<string>.Failure("Язык не найден");

        language.Delete();

        await languageRepository.DeleteAsync(language.Id, cancellationToken);
        
        await mediator.Publish(new LanguageDeletedEvent(language.Id), cancellationToken);

        return Result<string>.Success("Язык удален");
    }
}