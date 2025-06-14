using Airbnb.MongoRepository.Interfaces;
using Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.QueryObjects;
using Airbnb.UserManagement.Application.BoundedContexts.UserLangauageManagement.QueryObjects;
using Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Events;
using Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Interfaces;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserLangauageManagement.Consumers;

public class UserLanguagesUpdatedEventHandler(
    ILanguageRepository languageRepository,
    IProjectionRepository<UserEntityInfo> projectionRepository)
    : INotificationHandler<UserLanguagesUpdatedEvent>
{
    public async Task Handle(UserLanguagesUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var userInfo = await projectionRepository.FindByIdAsync(notification.UserId, cancellationToken);
        if (userInfo is null)
        {
            return;
        }

        var languageNames = notification.LanguageNames?.Distinct(StringComparer.OrdinalIgnoreCase);
        var languageInfos = new List<Language?>();

        foreach (var name in languageNames)
        {
            var language = await languageRepository.GetByNameAsync(name, cancellationToken);
            if (language is not null)
            {
                languageInfos.Add(new Language{ Id = language.Id, Name = language.Name });
            }
        }

        userInfo.Languages = languageInfos;
        await projectionRepository.UpdateAsync(userInfo, cancellationToken);
    }
}