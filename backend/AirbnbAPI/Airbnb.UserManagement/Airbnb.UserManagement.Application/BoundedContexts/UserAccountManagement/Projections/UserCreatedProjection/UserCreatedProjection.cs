using Airbnb.MongoRepository.Interfaces;
using Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.QueryObjects;
using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Events;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Projections.UserCreatedProjection;

public class UserCreatedProjection : INotificationHandler<UserCreatedEvent>
{
    private readonly IProjectionRepository<UserEntityInfo> _repository;

    public UserCreatedProjection(IProjectionRepository<UserEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(UserCreatedEvent @event, CancellationToken cancellationToken)
    {
        var user = new UserEntityInfo
        {
            Id = @event.AggregateId,
            FullName = @event.FullName,
            Email = @event.Email,
            // Role = @event.Role,
            // DateOfBirth = @event.DateOfBirth
        };

        await _repository.InsertAsync(user);
    }
}