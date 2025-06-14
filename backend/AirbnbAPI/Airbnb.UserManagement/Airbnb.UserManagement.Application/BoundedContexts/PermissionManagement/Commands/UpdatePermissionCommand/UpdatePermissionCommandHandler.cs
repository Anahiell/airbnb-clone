using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Events.Permission;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Interfaces;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.PermissionManagement.Commands.UpdatePermissionCommand;

public class UpdatePermissionCommandHandler(IPermissionRepository permissionRepository, IMediator _mediator)
    : ICommandHandler<UpdatePermissionCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
    {
        var permission = await permissionRepository.GetByIdAsync(request.Id, cancellationToken);

        if (permission is null)
            return Result<string>.Failure("Разрешение не найдено");

        permission.UpdatePermission(request.Name);

        await permissionRepository.UpdateAsync(permission, cancellationToken);
        
        await _mediator.Publish(new PermissionUpdatedEvent(permission.Id, permission.Permission), cancellationToken);

        return Result<string>.Success("Разрешение обновлено");
    }
}