using MediatR;

namespace Airbnb.PictureManagement.Application.BoundedContext.ProductPictureManagement.ProductPictureUpdatedConsumer.ProductPicturePublisher;

public interface IProductPictureEventDispatcher
{
    Task DispatchAsync(INotification evt, CancellationToken cancellationToken);
}