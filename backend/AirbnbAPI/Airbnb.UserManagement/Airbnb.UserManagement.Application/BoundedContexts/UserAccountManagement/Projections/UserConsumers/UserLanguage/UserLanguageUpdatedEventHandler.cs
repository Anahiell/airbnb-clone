using Airbnb.MongoRepository.Interfaces;
using Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.QueryObjects;
using Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Events;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Projections.UserRoleCreatedEventHandler;

public class UserLanguageUpdatedEventHandler(IProjectionRepository<UserEntityInfo> projectionRepository)
    : INotificationHandler<UserLanguageUpdatedEvent>
{
    public async Task Handle(UserLanguageUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var user = await projectionRepository.FindByIdAsync(notification.UserId, cancellationToken);
        if (user is null)
        {
            return;
        }

        user.UpdateLanguage(new Language
        {
            Id = notification.NewLanguageId,
            Name = notification.Name,
        });

        await projectionRepository.UpdateAsync(user, cancellationToken);
    }
}