using Airbnb.MongoRepository.Interfaces;
using Airbnb.UserManagement.Application.BoundedContexts.UserLangauageManagement.QueryObjects;
using Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Events;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserLangauageManagement.Projections.UserLanguageCreatedProjection;


public class UserLanguageUpdatedProjection : INotificationHandler<UserLanguageUpdatedEvent>
{
    private readonly IProjectionRepository<UserLanguageEntityInfo> _repository;

    public UserLanguageUpdatedProjection(IProjectionRepository<UserLanguageEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(UserLanguageUpdatedEvent @event, CancellationToken cancellationToken)
    {
        var updated = new UserLanguageEntityInfo
        {
            Id = @event.AggregateId,
            UserId = @event.UserId,
            LanguageId = @event.NewLanguageId,
            LanguageName = @event.Name,
        };

        await _repository.UpdateAsync(updated);
    }
}