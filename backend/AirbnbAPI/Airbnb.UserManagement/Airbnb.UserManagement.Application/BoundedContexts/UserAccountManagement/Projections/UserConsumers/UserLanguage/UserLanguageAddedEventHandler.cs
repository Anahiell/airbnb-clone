using Airbnb.MongoRepository.Interfaces;
using Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.QueryObjects;
using Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Events;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Projections.UserRoleCreatedEventHandler;

public class UserLanguageAddedEventHandler(IProjectionRepository<UserEntityInfo> projectionRepository)
    : INotificationHandler<UserLanguageCreatedEvent>
{
    public async Task Handle(UserLanguageCreatedEvent notification, CancellationToken cancellationToken)
    {
        var user = await projectionRepository.FindByIdAsync(notification.UserId, cancellationToken);
        if (user is null)
        {
            return;
        }

        user.UpdateLanguage(new Language{ Id = notification.LanguageId, Name = notification.Name });
        await projectionRepository.UpdateAsync(user, cancellationToken);
    }
}