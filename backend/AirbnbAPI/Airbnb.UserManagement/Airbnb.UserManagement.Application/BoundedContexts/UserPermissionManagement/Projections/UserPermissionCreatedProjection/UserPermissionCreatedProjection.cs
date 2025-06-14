using Airbnb.MongoRepository.Interfaces;
using Airbnb.UserManagement.Application.BoundedContexts.UserPermissionManagement.QueryObjects;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Events.UserPermission;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserPermissionManagement.Projections.UserPermissionCreatedProjection;


public class UserPermissionCreatedProjection : INotificationHandler<UserPermissionCreatedEvent>
{
    private readonly IProjectionRepository<UserPermissionEntityInfo> _repository;

    public UserPermissionCreatedProjection(IProjectionRepository<UserPermissionEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(UserPermissionCreatedEvent @event, CancellationToken cancellationToken)
    {
        var entity = new UserPermissionEntityInfo
        {
            Id = @event.AggregateId,
            UserId = @event.UserId,
            PermissionId = @event.PermissionId,
        };

        await _repository.InsertAsync(entity);
    }
}