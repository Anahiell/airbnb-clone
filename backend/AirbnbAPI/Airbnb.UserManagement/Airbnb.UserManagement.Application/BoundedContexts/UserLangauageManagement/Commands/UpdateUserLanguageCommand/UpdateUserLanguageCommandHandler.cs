using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.SharedKernel.Repositories;
using Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Aggregates;
using Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Events;
using Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Interfaces;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserLangauageManagement.Commands.UpdateUserLanguageCommand;

public class UpdateUserLanguageCommandHandler(
    IUserLanguageRepository userLanguageRepository,
    ILanguageRepository languageRepository,
    IMediator mediator)
    : ICommandHandler<UpdateUserLanguageCommand, Result<int>>
{
    public async Task<Result<int>> Handle(UpdateUserLanguageCommand request, CancellationToken cancellationToken)
    {
        var userLanguage = await userLanguageRepository.GetByUserIdAndLanguageIdAsync(request.Id, request.OldLanguageId, cancellationToken);
        if (userLanguage is null)
            return Result<int>.Failure("Язык пользователя не найден");

        var language = await languageRepository.GetByIdAsync(request.OldLanguageId, cancellationToken);
        if (language is null)
            return Result<int>.Failure("Указан неверный язык");
        
        userLanguage.UpdateLanguage(language.Id);

        await userLanguageRepository.UpdateAsync(userLanguage, cancellationToken);

        await mediator.Publish(new UserLanguageUpdatedEvent(userLanguage.Id, userLanguage.UserId, userLanguage.LanguageId, language.Name), cancellationToken);

        return Result<int>.Success(1);
    }
}