using Airbnb.MongoRepository.Interfaces;
using Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.QueryObjects;
using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Events;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Projections.UserUpdatedProjection;

public class UserUpdatedProjection : INotificationHandler<UserUpdatedEvent>
{
    private readonly IProjectionRepository<UserEntityInfo> _repository;

    public UserUpdatedProjection(IProjectionRepository<UserEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(UserUpdatedEvent @event, CancellationToken cancellationToken)
    {
        var updatedUser = new UserEntityInfo
        {
            Id = @event.AggregateId,
            FullName = @event.FullName,
            Email = @event.Email,
            // Role = @event.Role
            // = @event.DateOfBirth
        };

        await _repository.UpdateAsync(updatedUser); // Cancellation Token
    }
}