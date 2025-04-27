using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Services.Saga;

public class SagaTimeoutService : BackgroundService
{
    private readonly IServiceProvider _provider;
    private readonly ILogger<SagaTimeoutService> _logger;

    public SagaTimeoutService(IServiceProvider provider, ILogger<SagaTimeoutService> logger)
    {
        _provider = provider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _provider.CreateScope();
            var store = scope.ServiceProvider.GetRequiredService<ISagaStore>();

            // ⚠️ Здесь предполагается, что EF-саги или InMemory возвращают ExpireAt
            var sagas = await GetExpiringSagas(store, stoppingToken);
            foreach (var saga in sagas)
            {
                if (saga.ExpireAt <= DateTime.UtcNow && !saga.IsCompleted)
                {
                    _logger.LogWarning("Saga {Id} expired, compensating...", saga.Id);
                    await saga.CompensateAsync(stoppingToken);
                    await store.CompleteAsync(saga.Id, stoppingToken);
                }
            }

            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }

    private async Task<IEnumerable<ISaga>> GetExpiringSagas(ISagaStore store, CancellationToken token)
    {
        // Этот метод можно реализовать как extension к конкретной реализации
        return Enumerable.Empty<ISaga>(); // заглушка
    }
}
