using Airbnb.MongoRepository.Interfaces;
using Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.QueryObjects;
using Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Events;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Projections.UserRoleCreatedEventHandler;

public class UserLanguageDeletedEventHandler(IProjectionRepository<UserEntityInfo> projectionRepository)
    : INotificationHandler<UserLanguageRemovedEvent>
{
    public async Task Handle(UserLanguageRemovedEvent notification, CancellationToken cancellationToken)
    {
        var user = await projectionRepository.FindByIdAsync(notification.UserId, cancellationToken);
        if (user is null)
        {
            return;
        }

        user.Languages.RemoveAll(x => x.Id == notification.AggregateId);
        await projectionRepository.UpdateAsync(user, cancellationToken);
    }
}