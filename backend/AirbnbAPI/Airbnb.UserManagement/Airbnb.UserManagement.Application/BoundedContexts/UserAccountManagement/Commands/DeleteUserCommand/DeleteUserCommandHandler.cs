using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.SharedKernel.Repositories;
using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Aggregates;
using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Events;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Commands.DeleteUserCommand;

public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand, Result>
{
    private readonly IRepository<DomainUser> _userRepository;
    private readonly IMediator _mediator;

    public DeleteUserCommandHandler(IRepository<DomainUser> userRepository, IMediator mediator)
    {
        _userRepository = userRepository;
        _mediator = mediator;
    }

    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
        if (user == null)
        {
            // return Result.Failure("Пользователь не найден");
        }

        await _userRepository.DeleteAsync(request.Id, cancellationToken);

        await _mediator.Publish(new UserDeletedEvent(request.Id), cancellationToken);

        return Result.Success();
    }
}