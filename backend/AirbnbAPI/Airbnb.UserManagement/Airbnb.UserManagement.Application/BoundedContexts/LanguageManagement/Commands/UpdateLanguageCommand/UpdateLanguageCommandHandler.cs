using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Events;
using Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Interfaces;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.LanguageManagement.Commands.UpdateLanguageCommand;


public class UpdateLanguageCommandHandler(ILanguageRepository languageRepository, IMediator mediator)
    : ICommandHandler<UpdateLanguageCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateLanguageCommand request, CancellationToken cancellationToken)
    {
        var language = await languageRepository.GetByIdAsync(request.Id, cancellationToken);

        if (language is null)
            return Result<string>.Failure("Язык не найден");

        language.UpdateName(request.Name);

        await languageRepository.UpdateAsync(language, cancellationToken);
        
        await mediator.Publish(new LanguageUpdatedEvent(language.Id, language.Name), cancellationToken);

        return Result<string>.Success("Язык обновлён");
    }
}