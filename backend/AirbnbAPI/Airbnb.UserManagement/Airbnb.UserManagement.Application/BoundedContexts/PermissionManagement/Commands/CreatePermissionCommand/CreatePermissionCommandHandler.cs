using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Aggregates;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Events.Permission;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Interfaces;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.PermissionManagement.Commands.CreatePermissionCommand;

public class CreatePermissionCommandHandler(
    IPermissionRepository permissionRepository,
    IMediator mediator)
    : ICommandHandler<CreatePermissionCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
    {
        var permission = new DomainPermission(request.Name);

        var result = await permissionRepository.AddAsync(permission, cancellationToken);

        await mediator.Publish(new PermissionCreatedEvent(permission.Id, permission.Permission), cancellationToken);

        return Result<int>.Success(result);
    }
}