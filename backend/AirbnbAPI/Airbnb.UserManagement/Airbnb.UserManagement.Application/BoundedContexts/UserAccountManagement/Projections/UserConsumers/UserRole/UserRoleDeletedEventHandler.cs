using Airbnb.MongoRepository.Interfaces;
using Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.QueryObjects;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Events.UserRole;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Projections.UserRoleCreatedEventHandler;


public class UserRoleDeletedEventHandler(IProjectionRepository<UserEntityInfo> projectionRepository)
    : INotificationHandler<UserRoleDeletedEvent>
{
    public async Task Handle(UserRoleDeletedEvent notification, CancellationToken cancellationToken)
    {
        var user = await projectionRepository.FindByIdAsync(notification.UserId, cancellationToken);
        if (user is null)
        {
            return;
        }

        user.Roles?.RemoveAll(x => x?.Id == notification.RoleId);
        await projectionRepository.UpdateAsync(user, cancellationToken);
    }
}