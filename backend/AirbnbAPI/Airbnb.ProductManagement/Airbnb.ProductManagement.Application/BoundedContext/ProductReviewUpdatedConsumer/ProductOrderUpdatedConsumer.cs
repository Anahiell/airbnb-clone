using Airbnb.Domain;
using Airbnb.Domain.BoundedContexts.ProductManagement.Events;
using Airbnb.ProductManagement.Application.BoundedContext.Events;
using Airbnb.SharedKernel.Repositories;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductReviewUpdatedConsumer;

public class ProductOrderUpdatedConsumer : IConsumer<ProductOrderUpdatedEvent>
{
    private readonly IRepository<DomainProduct> _productRepository;
    private readonly ILogger<ProductOrderUpdatedConsumer> _logger;
    private readonly IMediator _mediator;

    public ProductOrderUpdatedConsumer(IRepository<DomainProduct> productRepository, ILogger<ProductOrderUpdatedConsumer> logger, IMediator mediator)
    {
        _productRepository = productRepository;
        _logger = logger;
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<ProductOrderUpdatedEvent> context)
    {
        var order = context.Message;
        _logger.LogInformation("Starting to process ProductOrderUpdatedEvent...");

        try
        {
            _logger.LogInformation("Processing ProductOrderUpdatedEvent for ProductId: {ProductId}, OrderId: {OrderId}, UserId: {UserId}, Start: {Start}, End: {End}",
                order.ProductId, order.Id, order.UserId, order.DateStart, order.DateEnd);

            var product = await _productRepository.GetByIdAsync(order.ProductId, context.CancellationToken);

            await _mediator.Publish(new ProductUpdatedEvent(
                product.Id,
                product.Title,
                product.Description,
                product.Price,
                product.IsAvailable,
                product.CreatedAt,
                product.UserId,
                product.ApartmentTypeId,
                product.AddressLegalId
            ), context.CancellationToken);

            _logger.LogInformation("Successfully processed ProductOrderUpdatedEvent for ProductId: {ProductId}, OrderId: {OrderId}", order.ProductId, order.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing ProductOrderUpdatedEvent.");
            throw;
        }

        _logger.LogInformation("Finished processing ProductOrderUpdatedEvent.");
    }
}