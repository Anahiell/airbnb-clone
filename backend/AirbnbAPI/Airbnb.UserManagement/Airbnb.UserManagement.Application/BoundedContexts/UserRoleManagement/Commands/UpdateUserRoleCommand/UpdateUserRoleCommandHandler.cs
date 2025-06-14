using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Aggregates;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Events.UserPermission;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Interfaces;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserRoleManagement.Commands.UpdateUserRoleCommand;

public class UpdateUserRoleCommandHandler(IUserRoleRepository userRoleRepository, IRoleRepository roleRepository, IMediator _mediator)
    : ICommandHandler<UpdateUserRoleCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
    {
        var existing = await userRoleRepository.GetByUserIdAndRoleIdAsync(request.UserId, request.OldRoleId, cancellationToken);

        if (existing is null)
            return Result<string>.Failure("Исходная связь не найдена");
        
        var role = await roleRepository.GetByIdAsync(existing.RoleId, cancellationToken);
        if (role is null) 
            return Result<string>.Failure("Роль не найдена");

        existing.UpdateRole(request.NewRoleId);
        await userRoleRepository.UpdateAsync(existing, cancellationToken);

        await _mediator.Publish(new UserRoleUpdatedEvent(request.UserId, request.OldRoleId, request.NewRoleId, role.Name), cancellationToken);

        return Result<string>.Success("Связь обновлена");
    }
}