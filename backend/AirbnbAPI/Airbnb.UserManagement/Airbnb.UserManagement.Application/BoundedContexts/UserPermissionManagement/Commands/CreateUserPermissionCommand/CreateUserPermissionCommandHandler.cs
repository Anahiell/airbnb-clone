using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Aggregates;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Events.UserPermission;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Interfaces;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserPermissionManagement.Commands.CreateUserPermissionCommand;

public class CreateUserPermissionCommandHandler(
    IPermissionRepository permissionRepository,
    IUserPermissionRepository userPermissionRepository,
    IMediator mediator)
    : ICommandHandler<CreateUserPermissionCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateUserPermissionCommand request, CancellationToken cancellationToken)
    {
        var userPermission = new DomainUserPermission(request.UserId, request.PermissionId);

        var id = await userPermissionRepository.AddAsync(userPermission, cancellationToken);

        var permission = await permissionRepository.GetByIdAsync(userPermission.PermissionId, cancellationToken);

        await mediator.Publish(new UserPermissionCreatedEvent(id, userPermission.UserId, userPermission.PermissionId, permission?.Permission), cancellationToken);

        return Result<int>.Success(id);
    }
}