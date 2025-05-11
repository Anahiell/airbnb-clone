using Microsoft.AspNetCore.SignalR;

namespace WebApplication1.Consumers;

public class PropertyHub : Hub
{
    private readonly ILogger<PropertyHub> _logger;

    public PropertyHub(ILogger<PropertyHub> logger)
    {
        _logger = logger;
    }
    
    public async Task SubscribeToPage(string pageName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, pageName);
        _logger.LogInformation("✅ Connection {ConnectionId} subscribed to group {Group}", Context.ConnectionId, pageName);
    }

    public async Task UnsubscribeFromPage(string pageName)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, pageName);
    }
}