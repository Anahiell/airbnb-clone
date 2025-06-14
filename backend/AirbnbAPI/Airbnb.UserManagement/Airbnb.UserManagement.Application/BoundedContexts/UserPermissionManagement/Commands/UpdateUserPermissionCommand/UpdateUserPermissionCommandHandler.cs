using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Events.UserRole;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Interfaces;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserPermissionManagement.Commands.UpdateUserPermissionCommand;

public class UpdateUserPermissionCommandHandler(IUserPermissionRepository userPermissionRepository, IPermissionRepository permissionRepository, IMediator mediator)
    : ICommandHandler<UpdateUserPermissionCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateUserPermissionCommand request, CancellationToken cancellationToken)
    {
        var userPermission = await userPermissionRepository.GetByUserIdAndPermissionIdAsync(request.Id, request.OldPermissionId, cancellationToken);

        if (userPermission is null)
            return Result<string>.Failure("Пользовательское разрешение не найдено");
        
        var permission = await permissionRepository.GetByIdAsync(userPermission.PermissionId, cancellationToken);

        if (permission is null)
            return Result<string>.Failure("Право не найдено");
        
        userPermission.UpdatePermission(request.PermissionId);

        await userPermissionRepository.UpdateAsync(userPermission, cancellationToken);
        
        await mediator.Publish(new UserPermissionUpdatedEvent(userPermission.Id, userPermission.UserId, userPermission.PermissionId, permission.Permission), cancellationToken);

        return Result<string>.Success("Пользовательское разрешение обновлено");
    }
}