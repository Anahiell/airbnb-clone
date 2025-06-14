using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Aggregates;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Events.UserRole;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Interfaces;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserRoleManagement.Commands.CreateUserRoleCommand;

public class CreateUserRoleCommandHandler(
    IRoleRepository roleRepository,
    IUserRoleRepository userRoleRepository,
    IMediator mediator)
    : ICommandHandler<CreateUserRoleCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateUserRoleCommand request, CancellationToken cancellationToken)
    {
        var existing = await userRoleRepository.GetByUserIdAndRoleIdAsync(request.UserId, request.RoleId, cancellationToken);
        if (existing is not null)
            return Result<int>.Failure("Связь уже существует");

        var userRole = new DomainUserRole(request.UserId, request.RoleId);

        var userRoleId = await userRoleRepository.AddAsync(userRole, cancellationToken);
        
        var role = await roleRepository.GetByIdAsync(request.RoleId, cancellationToken);
        if (role is null) return Result<int>.Failure("Роль не найдена");

        await mediator.Publish(new UserRoleCreatedEvent(userRoleId, userRole.UserId, userRole.RoleId, role.Name), cancellationToken);

        return Result<int>.Success(1);
    }
}