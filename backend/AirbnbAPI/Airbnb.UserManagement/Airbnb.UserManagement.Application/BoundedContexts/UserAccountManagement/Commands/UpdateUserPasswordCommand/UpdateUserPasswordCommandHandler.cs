using Airbnb.Application.Results;
using Airbnb.SharedKernel.Repositories;
using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Aggregates;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Commands.UpdateUserPasswordCommand;


public class UpdateUserPasswordCommandHandler : IRequestHandler<UpdateUserPasswordCommand, Result<string>>
{
    private readonly IRepository<DomainUser> _userRepository;

    public UpdateUserPasswordCommandHandler(IRepository<DomainUser> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<string>> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
        if (user == null)
        {
            return Result<string>.Failure("Пользователь не найден");
        }

        if (!user.CheckPassword(request.CurrentPassword))
        {
            return Result<string>.Failure("Неверный текущий пароль");
        }

        user.SetPassword(request.NewPassword);

        await _userRepository.UpdateAsync(user, cancellationToken);

        return Result<string>.Success("Пароль успешно обновлен");
    }
}