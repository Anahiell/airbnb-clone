using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.SharedKernel.Repositories;
using Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Aggregates;
using Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Events;
using Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Interfaces;
using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Aggregates;
using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Interfaces;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserLangauageManagement.Commands.CreateUserLanguageCommand;

public class CreateUserLanguageCommandHandler(
    IUserLanguageRepository repository,
    IUserRepository userRepository,
    ILanguageRepository languageRepository,
    IMediator mediator
) : ICommandHandler<CreateUserLanguageCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateUserLanguageCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null) return Result<int>.Failure("Пользователь не найден");

        var language = await languageRepository.GetByIdAsync(request.LanguageId, cancellationToken);
        if (language is null) return Result<int>.Failure("Язык не найден");

        var userLanguage = new DomainUserLanguage(request.UserId, request.LanguageId);

        var result = await repository.AddAsync(userLanguage, cancellationToken);
        
        
        await mediator.Publish(new UserLanguageCreatedEvent(userLanguage.Id, userLanguage.UserId, userLanguage.LanguageId, DateTime.Now, language.Name), cancellationToken);
        
        return Result<int>.Success(result);
    }
}