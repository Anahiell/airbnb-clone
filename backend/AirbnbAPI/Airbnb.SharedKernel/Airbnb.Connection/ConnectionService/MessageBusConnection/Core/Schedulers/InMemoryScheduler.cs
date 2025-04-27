using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Services.Schedulers;

public class InMemoryScheduler : IMessageScheduler
{
    private readonly ILogger<InMemoryScheduler> _logger;
    private readonly IServiceProvider _services;

    public InMemoryScheduler(IServiceProvider services, ILogger<InMemoryScheduler> logger)
    {
        _services = services;
        _logger = logger;
    }

    public Task ScheduleAsync<T>(T message, DateTimeOffset executeAt) where T : class
    {
        var delay = executeAt - DateTimeOffset.Now;
        _ = Task.Delay(delay).ContinueWith(async _ =>
        {
            using var scope = _services.CreateScope();
            var bus = scope.ServiceProvider.GetRequiredService<MessageBusProvider>();
            var queueName = typeof(T).Name;
            await bus.PublishAsync(message, queueName);
        });

        _logger.LogInformation("Scheduled {Type} for {Time}", typeof(T).Name, executeAt);
        return Task.CompletedTask;
    }
}
