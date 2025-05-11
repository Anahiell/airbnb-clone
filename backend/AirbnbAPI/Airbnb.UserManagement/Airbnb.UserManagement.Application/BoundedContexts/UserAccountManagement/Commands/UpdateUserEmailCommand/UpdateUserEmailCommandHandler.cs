using Airbnb.Application.Results;
using Airbnb.SharedKernel.Repositories;
using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Aggregates;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Commands.ChangeUserPassword;

public class UpdateUserEmailCommandHandler : IRequestHandler<UpdateUserEmailCommand, Result<string>>
{
    private readonly IRepository<DomainUser> _userRepository;

    public UpdateUserEmailCommandHandler(IRepository<DomainUser> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<string>> Handle(UpdateUserEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
        if (user == null)
        {
            return Result<string>.Failure("Пользователь не найден");
        }

        user.UpdateEmail(request.Email);

        await _userRepository.UpdateAsync(user, cancellationToken);

        return Result<string>.Success("Email успешно обновлен");
    }
}