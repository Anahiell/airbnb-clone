using Airbnb.MongoRepository.Interfaces;
using Airbnb.UserManagement.Application.BoundedContexts.UserLangauageManagement.QueryObjects;
using Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Events;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserLangauageManagement.Projections.UserLanguageCreatedProjection;

public class UserLanguageCreatedProjection : INotificationHandler<UserLanguageCreatedEvent>
{
    private readonly IProjectionRepository<UserLanguageEntityInfo> _repository;

    public UserLanguageCreatedProjection(IProjectionRepository<UserLanguageEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(UserLanguageCreatedEvent @event, CancellationToken cancellationToken)
    {
        var entity = new UserLanguageEntityInfo
        {
            Id = @event.AggregateId,
            UserId = @event.UserId,
            LanguageId = @event.LanguageId,
            LanguageName = @event.Name,
        };

        await _repository.InsertAsync(entity);
    }
}