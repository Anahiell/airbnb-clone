using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.SharedKernel.Repositories;
using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Aggregates;
using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Events;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Commands.UserCreateCommand;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Result<int>>
{
    private readonly IRepository<DomainUser> _userRepository;
    private readonly IMediator _mediator;

    public CreateUserCommandHandler(IRepository<DomainUser> userRepository, IMediator mediator)
    {
        _userRepository = userRepository;
        _mediator = mediator;
    }

    public async Task<Result<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new DomainUser(request.FullName, request.Email, request.Roles, request.DateOfBirth);

        var result = await _userRepository.AddAsync(user, cancellationToken);

        await _mediator.Publish(new UserCreatedEvent(user.Id, user.FullName, user.Email, user.Roles, user.DateOfBirth), cancellationToken);

        return Result<int>.Success(result);
    }
}