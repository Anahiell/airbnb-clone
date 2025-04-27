namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Services.Saga;

public class IdempotentSagaStep<TSaga> : ISagaStateStep<TSaga>
    where TSaga : ISagaInstance
{
    private readonly ISagaStateStep<TSaga> _inner;
    private readonly ISagaIdempotencyService _idemp;

    public string Label => _inner.Label;

    public IdempotentSagaStep(ISagaStateStep<TSaga> inner, ISagaIdempotencyService idemp)
    {
        _inner = inner;
        _idemp = idemp;
    }

    public async Task ExecuteAsync(TSaga saga)
    {
        var key = $"{saga.CorrelationId}:{Label}";
        var etag = saga.ETag;

        if (await _idemp.IsHandledAsync(saga.CorrelationId, Label, etag))
            return;

        if (!await _idemp.AcquireLockAsync(saga.CorrelationId, Label))
            return;

        try
        {
            await _inner.ExecuteAsync(saga);
            await _idemp.MarkHandledAsync(saga.CorrelationId, Label, etag);
        }
        finally
        {
            await _idemp.ReleaseLockAsync(saga.CorrelationId, Label);
        }
    }

    public async Task CompensateAsync(TSaga saga)
    {
        if (_inner.CompensateAsync != null)
        {
            var key = $"{saga.CorrelationId}:{Label}:compensate";
            if (!await _idemp.AcquireLockAsync(saga.CorrelationId, key))
                return;

            try
            {
                await _inner.CompensateAsync(saga);
            }
            finally
            {
                await _idemp.ReleaseLockAsync(saga.CorrelationId, key);
            }
        }
    }
}