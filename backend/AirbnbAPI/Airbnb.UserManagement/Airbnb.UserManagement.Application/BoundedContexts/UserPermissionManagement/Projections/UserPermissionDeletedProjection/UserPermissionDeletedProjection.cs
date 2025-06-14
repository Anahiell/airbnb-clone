using Airbnb.MongoRepository.Interfaces;
using Airbnb.UserManagement.Application.BoundedContexts.UserPermissionManagement.QueryObjects;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Events.UserPermission;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserPermissionManagement.Projections.UserPermissionDeletedProjection;

public class UserPermissionDeletedProjection : INotificationHandler<UserPermissionDeletedEvent>
{
    private readonly IProjectionRepository<UserPermissionEntityInfo> _repository;

    public UserPermissionDeletedProjection(IProjectionRepository<UserPermissionEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(UserPermissionDeletedEvent @event, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(@event.AggregateId);
    }
}