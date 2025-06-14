using Airbnb.MongoRepository.Interfaces;
using Airbnb.UserManagement.Application.BoundedContexts.UserLangauageManagement.QueryObjects;
using Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Events;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.LanguageManagement.Projections.LanguageCreatedProjection;

public class LanguageCreatedProjection : INotificationHandler<LanguageCreatedEvent>
{
    private readonly IProjectionRepository<LanguageEntityInfo> _repository;

    public LanguageCreatedProjection(IProjectionRepository<LanguageEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(LanguageCreatedEvent @event, CancellationToken cancellationToken)
    {
        var entity = new LanguageEntityInfo
        {
            Id = @event.AggregateId,
            Name = @event.Name,
        };

        await _repository.InsertAsync(entity);
    }
}