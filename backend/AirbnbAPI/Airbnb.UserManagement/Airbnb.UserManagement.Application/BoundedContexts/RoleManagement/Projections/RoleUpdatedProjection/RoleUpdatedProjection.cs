using Airbnb.MongoRepository.Interfaces;
using Airbnb.UserManagement.Application.BoundedContexts.RoleManagement.QueryObjects;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Events.Role;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.RoleManagement.Projections.RoleCreatedProjection;

public class RoleUpdatedProjection : INotificationHandler<RoleUpdatedEvent>
{
    private readonly IProjectionRepository<RoleEntityInfo> _repository;

    public RoleUpdatedProjection(IProjectionRepository<RoleEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(RoleUpdatedEvent @event, CancellationToken cancellationToken)
    {
        var updated = new RoleEntityInfo
        {
            Id = @event.AggregateId,
            Name = @event.Name,
        };

        await _repository.UpdateAsync(updated, cancellationToken);
    }
}