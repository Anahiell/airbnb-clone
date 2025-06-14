using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.ProductManagement.Application.BoundedContext.Events;
using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Aggregates;
using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Events;
using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Interfaces;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Aggregates;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Events.UserRole;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Interfaces;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Commands.RegisterUserCommand;

public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Result<int>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMediator _mediator;
    private readonly IBus _bus;
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRoleRepository _userRoleRepository;
    public RegisterUserCommandHandler(IUserRepository userRepository, IMediator mediator, IBus bus, IRoleRepository roleRepository, IUserRoleRepository userRoleRepository)
    {
        _userRepository = userRepository;
        _mediator = mediator;
        _bus = bus;
        _roleRepository = roleRepository;
        _userRoleRepository = userRoleRepository;
    }

    public async Task<Result<int>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
        if (existingUser != null)
        {
            return Result<int>.Failure("Пользователь с таким email уже существует.");
        }

        var role = await _roleRepository.GetByNameAsync("Guest", cancellationToken);
        if (role is null)
        {
            return Result<int>.Failure($"Роль 'Guest' не найдена.");
        }

        var hasher = new PasswordHasher<DomainUser>();

        var newUser = new DomainUser(
            fullName: request.FullName,
            email: request.Email,
            dateOfBirth: request.DateOfBirth,
            username: request.Username
        );

        var hashedPassword = hasher.HashPassword(newUser, request.Password);
        newUser.SetPassword(hashedPassword);

        var userId = await _userRepository.AddAsync(newUser, cancellationToken);
        
        await _mediator.Publish(new UserRegisterEvent(newUser.Id, newUser.FullName, newUser.UserName, newUser.Email, newUser.UserRoles, newUser.UserPermissions), cancellationToken);

        var userRole = new DomainUserRole(newUser.Id, role.Id);
        var userRoleId = await _userRoleRepository.AddAsync(userRole, cancellationToken);
        await _mediator.Publish(new UserRoleCreatedEvent(userRoleId, userRole.UserId, userRole.RoleId, role.Name), cancellationToken);
        
        using var memoryStream = new MemoryStream();
        await request.UserPicture.CopyToAsync(memoryStream, cancellationToken);
        var pictureData = memoryStream.ToArray();

        await _bus.Publish(new UserPictureUpdatedEvent
        {
            UserId = userId,
            PictureData = pictureData
        }, cancellationToken);
        
        return Result<int>.Success(userId);
    }
}