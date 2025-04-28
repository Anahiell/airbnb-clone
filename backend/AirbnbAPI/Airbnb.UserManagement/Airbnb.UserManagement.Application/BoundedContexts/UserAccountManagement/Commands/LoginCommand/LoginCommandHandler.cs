using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.SharedKernel.Repositories;
using Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Services;
using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Aggregates;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Commands.LoginCommand;

public class LoginCommandHandler : ICommandHandler<LoginCommand, Result<string>>
{
    private readonly IRepository<DomainUser> _userRepository;
    private readonly ITokenService _jwtTokenService;

    public LoginCommandHandler(IRepository<DomainUser> userRepository, ITokenService jwtTokenService)
    {
        _userRepository = userRepository;
        _jwtTokenService = jwtTokenService;
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

        var token = _jwtTokenService.GenerateJwt(user);

        return Result<string>.Success(token);
    }
}