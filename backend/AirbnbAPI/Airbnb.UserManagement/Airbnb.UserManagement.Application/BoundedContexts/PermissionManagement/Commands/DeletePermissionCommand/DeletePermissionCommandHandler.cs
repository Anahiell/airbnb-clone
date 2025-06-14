using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Events.Permission;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Interfaces;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.PermissionManagement.Commands.DeletePermissionCommand;

public class DeletePermissionCommandHandler(IPermissionRepository permissionRepository, IMediator _mediator)
    : ICommandHandler<DeletePermissionCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeletePermissionCommand request, CancellationToken cancellationToken)
    {
        var permission = await permissionRepository.GetByIdAsync(request.Id, cancellationToken);

        if (permission is null)
            return Result<string>.Failure("Разрешение не найдено");

        permission.Delete();

        await permissionRepository.DeleteAsync(permission.Id, cancellationToken);
        
        await _mediator.Publish(new PermissionDeletedEvent(permission.Id), cancellationToken);

        return Result<string>.Success("Разрешение удалено");
    }
}