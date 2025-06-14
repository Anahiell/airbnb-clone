using Airbnb.MongoRepository.Interfaces;
using Airbnb.UserManagement.Application.BoundedContexts.UserLangauageManagement.QueryObjects;
using Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Events;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.LanguageManagement.Projections.LanguageDeletedProjection;

public class LanguageDeletedProjection : INotificationHandler<LanguageDeletedEvent>
{
    private readonly IProjectionRepository<LanguageEntityInfo> _repository;

    public LanguageDeletedProjection(IProjectionRepository<LanguageEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(LanguageDeletedEvent @event, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(@event.AggregateId);
    }
}