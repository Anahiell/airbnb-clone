using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Events.UserRole;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Interfaces;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserRoleManagement.Commands.DeleteUserRoleCommand;

public class DeleteUserRoleCommandHandler(IUserRoleRepository userRoleRepository, IMediator _mediator)
    : ICommandHandler<DeleteUserRoleCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteUserRoleCommand request, CancellationToken cancellationToken)
    {
        var existing = await userRoleRepository.GetByUserIdAndRoleIdAsync(request.UserId, request.RoleId, cancellationToken);

        if (existing is null)
            return Result<string>.Failure("Связь не найдена");

        await userRoleRepository.RemoveAsync(request.UserId, request.RoleId, cancellationToken);
        
        await _mediator.Publish(new UserRoleDeletedEvent(request.UserId, request.RoleId), cancellationToken);

        return Result<string>.Success("Связь удалена");
    }
}