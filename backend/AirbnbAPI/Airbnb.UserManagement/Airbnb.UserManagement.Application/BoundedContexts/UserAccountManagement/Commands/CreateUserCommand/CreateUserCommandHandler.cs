using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.SharedKernel.Repositories;
using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Aggregates;
using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Events;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Interfaces;
using Airbnb.UserManagement.Infrastructure.Repositories;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Commands.UserCreateCommand;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Result<int>>
{
    private readonly IRepository<DomainUser> _userRepository;
    private readonly IMediator _mediator;
    private readonly IRoleRepository _roleRepository;
    
    public CreateUserCommandHandler(IRepository<DomainUser> userRepository, IMediator mediator, IRoleRepository userRoleRepository)
    {
        _userRepository = userRepository;
        _mediator = mediator;
        _roleRepository = userRoleRepository;
    }

    public async Task<Result<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new DomainUser(request.FullName, request.Email, request.Username, request.DateOfBirth);

        var roles = new List<DomainRole>();
        foreach (var role in request.Roles)
        {
            var roleEntity = await _roleRepository.GetByNameAsync(role, cancellationToken);
            if (roleEntity == null)
            {
                return Result<int>.Failure("Некоторые роли не найдены.");
            }

            roles.Add(roleEntity);
        }
        
        var result = await _userRepository.AddAsync(user, cancellationToken);

        await _mediator.Publish(new UserCreatedEvent(user.Id, user.FullName, user.UserName, user.Email, user.UserRoles.ToList(), user.UserPermissions.ToList(), user.DateOfBirth), cancellationToken);

        return Result<int>.Success(result);
    }
}