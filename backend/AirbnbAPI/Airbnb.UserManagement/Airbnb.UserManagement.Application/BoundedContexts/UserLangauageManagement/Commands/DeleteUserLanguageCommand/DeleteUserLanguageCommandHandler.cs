using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.SharedKernel.Repositories;
using Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Aggregates;
using Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Events;
using Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Interfaces;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserLangauageManagement.Commands.DeleteUserLanguageCommand;

public class DeleteUserLanguageCommandHandler(
    IUserLanguageRepository userLanguageRepository,
    IMediator mediator)
    : ICommandHandler<DeleteUserLanguageCommand, Result<int>>
{
    public async Task<Result<int>> Handle(DeleteUserLanguageCommand request, CancellationToken cancellationToken)
    {
        var userLanguage = await userLanguageRepository.GetByUserIdAndLanguageIdAsync(request.UserId, request.Id, cancellationToken);
        if (userLanguage is null)
            return Result<int>.Failure("Запись языка пользователя не найдена");

        await userLanguageRepository.RemoveAsync(request.Id, request.UserId, cancellationToken);

        await mediator.Publish(new UserLanguageRemovedEvent(userLanguage.UserId, userLanguage.Id), cancellationToken);

        return Result<int>.Success(1);
    }
}