using Airbnb.MongoRepository.Interfaces;
using Airbnb.UserManagement.Application.BoundedContexts.PermissionManagement.QueryObjects;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Events.Permission;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.PermissionManagement.Projections.PermissionCreatedProjection;

public class PermissionCreatedProjection : INotificationHandler<PermissionCreatedEvent>
{
    private readonly IProjectionRepository<PermissionEntityInfo> _repository;

    public PermissionCreatedProjection(IProjectionRepository<PermissionEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(PermissionCreatedEvent @event, CancellationToken cancellationToken)
    {
        var entity = new PermissionEntityInfo
        {
            Id = @event.AggregateId,
            Name = @event.Permission,
        };

        await _repository.InsertAsync(entity);
    }
}