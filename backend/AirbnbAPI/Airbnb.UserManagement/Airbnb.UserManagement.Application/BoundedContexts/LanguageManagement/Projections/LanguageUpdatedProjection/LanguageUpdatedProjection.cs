using Airbnb.MongoRepository.Interfaces;
using Airbnb.UserManagement.Application.BoundedContexts.UserLangauageManagement.QueryObjects;
using Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Events;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.LanguageManagement.Projections.LanguageUpdatedProjection;


public class LanguageUpdatedProjection : INotificationHandler<LanguageUpdatedEvent>
{
    private readonly IProjectionRepository<LanguageEntityInfo> _repository;

    public LanguageUpdatedProjection(IProjectionRepository<LanguageEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(LanguageUpdatedEvent @event, CancellationToken cancellationToken)
    {
        var updated = new LanguageEntityInfo
        {
            Id = @event.AggregateId,
            Name = @event.NewName,
        };

        await _repository.UpdateAsync(updated);
    }
}