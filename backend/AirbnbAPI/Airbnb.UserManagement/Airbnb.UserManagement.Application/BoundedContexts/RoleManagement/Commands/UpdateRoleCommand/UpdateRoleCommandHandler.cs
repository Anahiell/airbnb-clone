using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Events.Role;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Interfaces;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.RoleManagement.Commands.UpdateRoleCommand;

public class UpdateRoleCommandHandler(IRoleRepository roleRepository, IMediator mediator)
    : ICommandHandler<UpdateRoleCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await roleRepository.GetByIdAsync(request.Id, cancellationToken);

        if (role is null)
            return Result<string>.Failure("Роль не найдена");

        role.UpdateName(request.Name);

        await roleRepository.UpdateAsync(role, cancellationToken);
        
        await mediator.Publish(new RoleUpdatedEvent(role.Id, role.Name), cancellationToken);

        return Result<string>.Success("Роль обновлена");
    }
}