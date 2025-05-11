using Airbnb.MongoRepository.Interfaces;
using Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Commands.RegisterUserCommand;
using Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.QueryObjects;
using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Events;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Projections.RegisterUserProjection;

public class RegisterUserProjection : INotificationHandler<UserRegisterEvent>
{
    private readonly IProjectionRepository<UserEntityInfo> _repository;
    private readonly IMediator _mediator;

    public RegisterUserProjection(IProjectionRepository<UserEntityInfo> repository, IMediator mediator)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mediator = mediator;
    }

    public async Task Handle(UserRegisterEvent @event, CancellationToken cancellationToken)
    {
        var user = new UserEntityInfo
        {
            Id = @event.AggregateId,
            FullName = @event.FullName,
            Email = @event.Email,
            // Role = @event.Role,
            // CreatedAt = @event.DateOfBirth
        };

        await _repository.InsertAsync(user);
    }
}