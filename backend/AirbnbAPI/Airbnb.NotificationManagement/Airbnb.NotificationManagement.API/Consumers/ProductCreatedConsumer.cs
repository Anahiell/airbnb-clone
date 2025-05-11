using Airbnb.ProductManagement.Application.BoundedContext.Events;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace WebApplication1.Consumers;

public class ProductCreatedConsumer : IConsumer<ProductSignalRCreatedEvent>
{
    private readonly IHubContext<PropertyHub> _hub;
    private readonly ILogger<ProductCreatedConsumer> _logger;

    public ProductCreatedConsumer(IHubContext<PropertyHub> hub, ILogger<ProductCreatedConsumer> logger)
    {
        _hub = hub;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ProductSignalRCreatedEvent> context)
    {
        try
        {
            await _hub.Clients.Group("Index")
                .SendAsync("ReceivePropertyUpdate", context.Message);
            _logger.LogInformation("Sent CREATE to Index via MassTransit");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "SignalR push CREATE failed");
        }
    }
}