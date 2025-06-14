using Airbnb.MongoRepository.Interfaces;
using Airbnb.UserManagement.Application.BoundedContexts.RoleManagement.QueryObjects;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Events.Role;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.RoleManagement.Projections.RoleCreatedProjection;

public class RoleCreatedProjection : INotificationHandler<RoleCreatedEvent>
{
    private readonly IProjectionRepository<RoleEntityInfo> _repository;

    public RoleCreatedProjection(IProjectionRepository<RoleEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(RoleCreatedEvent @event, CancellationToken cancellationToken)
    {
        var entity = new RoleEntityInfo
        {
            Id = @event.AggregateId,
            Name = @event.Name,
        };

        await _repository.InsertAsync(entity);
    }
}