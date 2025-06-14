using Airbnb.MongoRepository.Interfaces;
using Airbnb.UserManagement.Application.BoundedContexts.UserRoleManagement.QueryObjects;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Events.UserRole;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserRoleManagement.Projections.UserRoleCreatedProjection;

public class UserRoleCreatedProjection : INotificationHandler<UserRoleCreatedEvent>
{
    private readonly IProjectionRepository<UserRoleEntityInfo> _repository;

    public UserRoleCreatedProjection(IProjectionRepository<UserRoleEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(UserRoleCreatedEvent @event, CancellationToken cancellationToken)
    {
        var entity = new UserRoleEntityInfo
        {
            Id = @event.Id,
            UserId = @event.UserId,
            RoleId = @event.RoleId
        };

        await _repository.InsertAsync(entity);
    }
}