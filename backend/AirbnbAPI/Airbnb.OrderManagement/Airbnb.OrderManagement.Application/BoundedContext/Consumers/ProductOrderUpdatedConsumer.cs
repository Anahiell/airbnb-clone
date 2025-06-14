using Airbnb.OrderManagement.Domain.BoundedContexts.OrderManagement.Aggregates;
using Airbnb.OrderManagement.Domain.BoundedContexts.OrderManagement.Events;
using Airbnb.ProductManagement.Application.BoundedContext.Events;
using Airbnb.SharedKernel.Repositories;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Airbnb.OrderManagement.Application.BoundedContext.Consumers;

public class ProductOrderUpdatedConsumer : IConsumer<ProductOrderUpdatedEvent>
{
    private readonly IRepository<DomainOrder> _orderRepository;
    private readonly ILogger<ProductOrderUpdatedConsumer> _logger;
    private readonly IMediator _mediator;

    public ProductOrderUpdatedConsumer(
        IRepository<DomainOrder> orderRepository,
        ILogger<ProductOrderUpdatedConsumer> logger,
        IMediator mediator)
    {
        _orderRepository = orderRepository;
        _logger = logger;
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<ProductOrderUpdatedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Processing ProductOrderUpdatedEvent...");

        try
        {
            var order = new DomainOrder(message.ProductId, message.UserId, message.DateStart, message.DateEnd);

            await _orderRepository.UpdateAsync(order, context.CancellationToken);

            await _mediator.Publish(new OrderUpdatedEvent(
                order.Id, order.ProductId, order.UserId, order.DateStart, order.DateEnd), context.CancellationToken);

            _logger.LogInformation($"Processed order update for ProductId: {order.ProductId}, OrderId: {order.Id}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while processing ProductOrderUpdatedEvent.");
            throw;
        }
    }
}