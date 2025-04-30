using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Aggregates;
using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Commands.RegisterUserCommand;

public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Result<int>>
{
    private readonly IUserRepository _userRepository;

    public RegisterUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<int>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
        if (existingUser != null)
        {
            // return Result<int>.Failure("Пользователь с таким email уже существует.");
        }
        
        var hasher = new PasswordHasher<DomainUser>();

        var newUser = new DomainUser(
            fullName: request.FullName,
            email: request.Email,
            roles: request.Roles.Select(role => Enum.TryParse<UserRole>(role, out var parsedRole) ? parsedRole : UserRole.Customer).ToList(),
            dateOfBirth: request.DateOfBirth
        );
        
        var hashedPassword = hasher.HashPassword(newUser, request.Password);
        newUser.SetPassword(hashedPassword);

        var userId = await _userRepository.AddAsync(newUser, cancellationToken);

        return Result<int>.Success(userId);
    }
}