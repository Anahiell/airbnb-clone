using Airbnb.MongoRepository.Interfaces;
using Airbnb.UserManagement.Application.BoundedContexts.UserRoleManagement.QueryObjects;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Events.UserPermission;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserRoleManagement.Projections.UserRoleUpdatedProjection;

public class UserRoleUpdatedProjection : INotificationHandler<UserRoleUpdatedEvent>
{
    private readonly IProjectionRepository<UserRoleEntityInfo> _repository;

    public UserRoleUpdatedProjection(IProjectionRepository<UserRoleEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(UserRoleUpdatedEvent @event, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(@event.AggregateId);

        var updated = new UserRoleEntityInfo
        {
            Id = @event.AggregateId,
            UserId = @event.UserId,
            RoleId = @event.NewRoleId
        };

        await _repository.InsertAsync(updated);
    }
}