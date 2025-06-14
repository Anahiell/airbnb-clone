using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Events.Role;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Interfaces;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.RoleManagement.Commands.CreateRoleCommand;

public class CreateRoleCommandHandler(
    IRoleRepository roleRepository,
    IMediator mediator)
    : ICommandHandler<CreateRoleCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = new DomainRole(request.Name);

        var result = await roleRepository.AddAsync(role, cancellationToken);

        await mediator.Publish(new RoleCreatedEvent(role.Id, role.Name), cancellationToken);

        return Result<int>.Success(result);
    }
}