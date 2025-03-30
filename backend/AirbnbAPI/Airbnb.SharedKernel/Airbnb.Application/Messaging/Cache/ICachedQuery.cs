using Airbnb.Application.Results;
using MediatR;

namespace Airbnb.Application.Messaging.Cache;

public interface ICachedQuery<TResponse> : IQuery<TResponse>, ICachedQuery
{
    IEnumerable<object> ExtractCacheableItems(TResponse response);
}

public interface ICachedQuery
{
    string Key { get; }

    TimeSpan? Expiration { get; }
}