using Airbnb.MongoRepository.Interfaces;
using Airbnb.UserManagement.Application.BoundedContexts.UserPermissionManagement.QueryObjects;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Events.UserRole;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserPermissionManagement.Projections.UserPermissionUpdatedProjection;


public class UserPermissionUpdatedProjection : INotificationHandler<UserPermissionUpdatedEvent>
{
    private readonly IProjectionRepository<UserPermissionEntityInfo> _repository;

    public UserPermissionUpdatedProjection(IProjectionRepository<UserPermissionEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(UserPermissionUpdatedEvent @event, CancellationToken cancellationToken)
    {
        var updated = new UserPermissionEntityInfo
        {
            Id = @event.AggregateId,
            UserId = @event.UserId,
            PermissionId = @event.NewPermissionId,
        };

        await _repository.UpdateAsync(updated, cancellationToken);
    }
}