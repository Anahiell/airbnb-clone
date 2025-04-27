using Airbnb.Connection.ConnectionService.MessageBusConnection.Interfaces;

namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Services.Batching;

public class BatchingPublisher : IMessagePublisher
{
    private readonly List<object> _buffer = new();
    private readonly Timer _timer;
    private readonly int _maxBatchSize;
    private readonly TimeSpan _flushInterval;
    private readonly IMessageBusProvider _inner;

    public BatchingPublisher(IMessageBusProvider inner, int maxBatchSize, TimeSpan flushInterval)
    {
        _inner = inner;
        _maxBatchSize = maxBatchSize;
        _flushInterval = flushInterval;
        _timer = new Timer(_ => Flush(), null, flushInterval, flushInterval);
    }

    public Task PublishAsync<T>(T message) where T : class
    {
        lock (_buffer)
        {
            _buffer.Add(message);
            if (_buffer.Count >= _maxBatchSize)
            {
                Flush();
            }
        }

        return Task.CompletedTask;
    }

    private void Flush()
    {
        List<object> batch;
        lock (_buffer)
        {
            if (!_buffer.Any()) return;
            batch = new List<object>(_buffer);
            _buffer.Clear();
        }

        foreach (var msg in batch)
        {
            var method = typeof(IMessageBusProvider).GetMethod("PublishAsync")!.MakeGenericMethod(msg.GetType());
            method.Invoke(_inner, new[] { msg });
        }
    }
}
