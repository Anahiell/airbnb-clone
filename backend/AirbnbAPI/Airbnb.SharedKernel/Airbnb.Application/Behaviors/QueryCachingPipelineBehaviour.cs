using Airbnb.Application.Messaging.Cache;
using Airbnb.Cache;
using MediatR;

namespace Airbnb.Application.Behaviors;

public class QueryCachingPipelineBehaviour<TRequest, TResponse>(ICacheService cacheService)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICachedQuery
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        Console.WriteLine(request.Key + request.Expiration);
        return await cacheService.GetOrCreateAsync(request.Key, _ => next(), request.Expiration, cancellationToken);
    }
}