using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Events.Role;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Interfaces;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.RoleManagement.Commands.DeleteRoleCommand;

public class DeleteRoleCommandHandler(IRoleRepository roleRepository, IMediator _mediator)
    : ICommandHandler<DeleteRoleCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await roleRepository.GetByIdAsync(request.Id, cancellationToken);

        if (role is null)
            return Result<string>.Failure("Роль не найдена");

        role.Delete();

        await roleRepository.DeleteAsync(role.Id, cancellationToken);
        
        await _mediator.Publish(new RoleDeletedEvent(role.Id), cancellationToken);

        return Result<string>.Success("Роль удалена");
    }
}