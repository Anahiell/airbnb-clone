using Airbnb.MongoRepository.Interfaces;
using Airbnb.UserManagement.Application.BoundedContexts.RoleManagement.QueryObjects;
using Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.QueryObjects;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Events.UserRole;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Projections.UserRoleCreatedEventHandler;

public class UserRoleCreatedEventHandler(IProjectionRepository<UserEntityInfo> projectionRepository)
    : INotificationHandler<UserRoleCreatedEvent>
{
    public async Task Handle(UserRoleCreatedEvent notification, CancellationToken cancellationToken)
    {
        var user = await projectionRepository.FindByIdAsync(notification.UserId, cancellationToken);
        if (user is null)
        {
            return;
        }

        user.UpdateRole(new Role{ Id = notification.RoleId, Name = notification.Name });
        await projectionRepository.UpdateAsync(user, cancellationToken);
    }
}