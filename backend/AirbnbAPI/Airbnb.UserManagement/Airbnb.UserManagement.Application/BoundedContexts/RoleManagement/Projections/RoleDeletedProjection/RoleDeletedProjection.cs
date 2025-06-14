using Airbnb.MongoRepository.Interfaces;
using Airbnb.UserManagement.Application.BoundedContexts.RoleManagement.QueryObjects;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Events.Role;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.RoleManagement.Projections.RoleCreatedProjection;

public class RoleDeletedProjection : INotificationHandler<RoleDeletedEvent>
{
    private readonly IProjectionRepository<RoleEntityInfo> _repository;

    public RoleDeletedProjection(IProjectionRepository<RoleEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(RoleDeletedEvent @event, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(@event.AggregateId);
    }
}