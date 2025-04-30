using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.SharedKernel.Repositories;
using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Aggregates;
using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Events;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Commands.UpdateUserCommand;

public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, Result>
{
    private readonly IRepository<DomainUser> _userRepository;
    private readonly IMediator _mediator;

    public UpdateUserCommandHandler(IRepository<DomainUser> userRepository, IMediator mediator)
    {
        _userRepository = userRepository;
        _mediator = mediator;
    }

    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
        if (user == null)
        {
            // return Result.Failure("Пользователь не найден");
        }

        user.UpdateUser(request.FullName, request.Email, request.Roles, request.DateOfBirth);

        await _userRepository.UpdateAsync(user, cancellationToken);

        await _mediator.Publish(new UserUpdatedEvent(user.Id, user.FullName, user.Email, user.Roles, user.DateOfBirth), cancellationToken);

        return Result.Success();
    }
}