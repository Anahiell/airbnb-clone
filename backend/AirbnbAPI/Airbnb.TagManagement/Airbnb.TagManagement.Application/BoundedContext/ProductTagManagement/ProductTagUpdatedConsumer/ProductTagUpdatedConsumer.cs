using Airbnb.ProductManagement.Application.BoundedContext.Events;
using Airbnb.TagsManagement.Domain.BoundedContexts.ProductTagManagement.Aggregates;
using Airbnb.TagsManagement.Domain.BoundedContexts.ProductTagManagement.Interfaces;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Airbnb.TagsManagement.Application.BoundedContext.ProductTagManagement.ProductTagUpdatedConsumer;

public class ProductTagUpdatedConsumer : IConsumer<ProductTagUpdatedEvent>
{
    private readonly IProductTagRepository _productTagRepository;
    private readonly ILogger<ProductTagUpdatedConsumer> _logger;
    private readonly IMediator _mediator;

    public ProductTagUpdatedConsumer(IProductTagRepository productTagRepository, ILogger<ProductTagUpdatedConsumer> logger, IMediator mediator)
    {
        _productTagRepository = productTagRepository;
        _logger = logger;
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<ProductTagUpdatedEvent> context)
    {
        var eventMessage = context.Message;
        _logger.LogInformation("Starting to process event...");
        try
        {
            _logger.LogInformation($"Received ProductTagUpdatedEvent for ProductId: {eventMessage.ProductId}, TagId: {eventMessage.ProductTags.Count}");

            var entity = new ProductTag(eventMessage.ProductId, int.Parse(eventMessage?.ProductTags?.FirstOrDefault()));

            // Добавляем новый ProductTag в репозиторий
            var result = await _productTagRepository.AddAsync(entity, CancellationToken.None);
            
            await _mediator.Publish(new Domain.BoundedContexts.ProductTagManagement.Events.ProductTagUpdatedEvent(
                entity.Id, entity.ProductId, entity.TagId), context.CancellationToken);

            _logger.LogInformation($"Successfully processed ProductTag with ProductId: {eventMessage.ProductId} and TagId: {eventMessage.ProductTags}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing ProductTagUpdatedEvent.");
            throw; // Повторное возбуждение исключения, если нужно будет повторить процесс.
        }
        _logger.LogInformation("Finished processing event.");
    }

}
