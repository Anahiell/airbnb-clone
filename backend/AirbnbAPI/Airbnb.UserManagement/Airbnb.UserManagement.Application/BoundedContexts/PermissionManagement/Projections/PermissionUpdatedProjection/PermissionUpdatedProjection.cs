using Airbnb.MongoRepository.Interfaces;
using Airbnb.UserManagement.Application.BoundedContexts.PermissionManagement.QueryObjects;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Events.Permission;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.PermissionManagement.Projections.PermissionUpdatedProjection;

public class PermissionUpdatedProjection : INotificationHandler<PermissionUpdatedEvent>
{
    private readonly IProjectionRepository<PermissionEntityInfo> _repository;

    public PermissionUpdatedProjection(IProjectionRepository<PermissionEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(PermissionUpdatedEvent @event, CancellationToken cancellationToken)
    {
        var updated = new PermissionEntityInfo
        {
            Id = @event.AggregateId,
            Name = @event.Permission,
        };

        await _repository.UpdateAsync(updated);
    }
}