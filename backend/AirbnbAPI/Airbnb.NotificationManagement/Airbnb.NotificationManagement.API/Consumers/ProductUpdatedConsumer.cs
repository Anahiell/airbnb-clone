using Airbnb.ProductManagement.Application.BoundedContext.Events;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace WebApplication1.Consumers;

public class ProductUpdatedConsumer : IConsumer<ProductSignalRUpdatedEvent>
{
    private readonly IHubContext<PropertyHub> _hub;
    private readonly ILogger<ProductUpdatedConsumer> _logger;

    public ProductUpdatedConsumer(IHubContext<PropertyHub> hub, ILogger<ProductUpdatedConsumer> logger)
    {
        _hub = hub;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ProductSignalRUpdatedEvent> context)
    {
        try
        {
            await _hub.Clients.Group("Details")
                .SendAsync("ReceivePropertyUpdate", context.Message);
            _logger.LogInformation("Sent UPDATE to Details via MassTransit");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "SignalR push UPDATE failed");
        }
    }
}