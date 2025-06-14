using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.MongoRepository.Repositories;
using Airbnb.SharedKernel.Repositories;
using Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.QueryObjects;
using Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Services;
using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Aggregates;
using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Interfaces;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Interfaces;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Commands.LoginCommand;

public class LoginCommandHandler : ICommandHandler<LoginCommand, Result<string>>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _jwtTokenService;
    private readonly IUserRoleRepository _userRoleRepository;

    public LoginCommandHandler(IUserRepository userRepository, ITokenService jwtTokenService, IUserRoleRepository userRoleRepository)
    {
        _userRepository = userRepository;
        _jwtTokenService = jwtTokenService;
        _userRoleRepository = userRoleRepository;
    }

    public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (user == null)
        {
            return Result<string>.Failure("Пользователь с таким email не найден.");
        }

        if (!user.CheckPassword(request.Password))
        {
            return Result<string>.Failure("Неверный пароль.");
        }

        var roles = await _userRoleRepository.GetByUserIdAsync(user.Id, cancellationToken);

        var token = _jwtTokenService.GenerateJwt(user, roles);

        return Result<string>.Success(token);
    }
}