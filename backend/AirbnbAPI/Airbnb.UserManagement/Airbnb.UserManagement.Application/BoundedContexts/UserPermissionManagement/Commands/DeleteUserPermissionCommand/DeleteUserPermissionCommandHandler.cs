using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Events.UserPermission;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Interfaces;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserPermissionManagement.Commands.DeleteUserPermissionCommand;

public class DeleteUserPermissionCommandHandler(IUserPermissionRepository userPermissionRepository, IMediator mediator)
    : ICommandHandler<DeleteUserPermissionCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteUserPermissionCommand request, CancellationToken cancellationToken)
    {
        var userPermission = await userPermissionRepository.GetByUserIdAndPermissionIdAsync(request.Id, request.PermissionId, cancellationToken);

        if (userPermission is null)
            return Result<string>.Failure("Пользовательское разрешение не найдено");

        userPermission.Remove();

        await userPermissionRepository.RemoveAsync(userPermission.UserId, userPermission.Id, cancellationToken);

        await mediator.Publish(new UserPermissionDeletedEvent(userPermission.Id), cancellationToken);

        
        return Result<string>.Success("Пользовательское разрешение удалено");
    }
}