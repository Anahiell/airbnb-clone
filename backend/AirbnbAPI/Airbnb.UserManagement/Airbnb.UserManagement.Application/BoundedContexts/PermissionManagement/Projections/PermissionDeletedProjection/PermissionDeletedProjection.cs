using Airbnb.MongoRepository.Interfaces;
using Airbnb.UserManagement.Application.BoundedContexts.PermissionManagement.QueryObjects;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Events.Permission;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.PermissionManagement.Projections.PermissionDeletedProjection;

public class PermissionDeletedProjection : INotificationHandler<PermissionDeletedEvent>
{
    private readonly IProjectionRepository<PermissionEntityInfo> _repository;

    public PermissionDeletedProjection(IProjectionRepository<PermissionEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(PermissionDeletedEvent @event, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(@event.AggregateId);
    }
}