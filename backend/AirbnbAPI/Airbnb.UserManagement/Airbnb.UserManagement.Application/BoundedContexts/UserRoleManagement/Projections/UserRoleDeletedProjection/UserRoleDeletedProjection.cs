using Airbnb.MongoRepository.Interfaces;
using Airbnb.UserManagement.Application.BoundedContexts.UserRoleManagement.QueryObjects;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Events.UserPermission;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Events.UserRole;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserRoleManagement.Projections.UserRoleDeletedProjection;

public class UserRoleDeletedProjection : INotificationHandler<UserRoleDeletedEvent>
{
    private readonly IProjectionRepository<UserRoleEntityInfo> _repository;

    public UserRoleDeletedProjection(IProjectionRepository<UserRoleEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(UserRoleDeletedEvent @event, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(@event.AggregateId);
    }
}