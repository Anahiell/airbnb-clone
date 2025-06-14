using MediatR;

namespace Airbnb.TagsManagement.Application.BoundedContext.ProductTagManagement.ProductTagUpdatedConsumer.Publisher;

public interface ITagEventDispatcher
{
    Task DispatchAsync(INotification evt, CancellationToken cancellationToken);
}