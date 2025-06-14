using Airbnb.MongoRepository.Interfaces;
using Airbnb.UserManagement.Application.BoundedContexts.UserLangauageManagement.QueryObjects;
using Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Events;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserLangauageManagement.Projections.UserLanguageDeletedProjection;

public class UserLanguageDeletedProjection : INotificationHandler<UserLanguageRemovedEvent>
{
    private readonly IProjectionRepository<UserLanguageEntityInfo> _repository;

    public UserLanguageDeletedProjection(IProjectionRepository<UserLanguageEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(UserLanguageRemovedEvent @event, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(@event.AggregateId);
    }
}