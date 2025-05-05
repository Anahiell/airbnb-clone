using Airbnb.MongoRepository.Interfaces;
using Airbnb.PictureManagement.Application.BoundedContext.QueryObjects;
using Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Events;
using Airbnb.PictureManagement.Domain.BoundedContexts.ProductPictureManagement.Events;
using MediatR;

namespace Airbnb.PictureManagement.Application.BoundedContext.Projections.PictureCreatedProjection;

public class PictureCreatedProjection : INotificationHandler<ProductPictureCreatedEvent>
{
    private readonly IProjectionRepository<PictureEntityInfo> _repository;

    public PictureCreatedProjection(IProjectionRepository<PictureEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(ProductPictureCreatedEvent @event, CancellationToken cancellationToken)
    {
        var picture = new PictureEntityInfo
        {
            Id = @event.Id,
            ProductId = @event.ProductId,
            Url = @event.Url,
            CreatedAt = @event.CreatedDate
        };

        await _repository.InsertAsync(picture);
    }
}