using Airbnb.ProductManagement.Application.BoundedContext.Queries;
using Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductEnricher;

public class ProductDataAggregator : IProductDataAggregator
{
    private readonly IEnumerable<IProductEnricher> _enrichers;

    public ProductDataAggregator(IEnumerable<IProductEnricher> enrichers)
    {
        _enrichers = enrichers;
    }

    public async Task<List<ProductEntityInfo>> EnrichAsync(GetProductPaginatedQuery request, List<ProductEntityInfo> products, CancellationToken cancellationToken)
    {
        foreach (var enricher in _enrichers)
        {
            await enricher.EnrichAsync(products, cancellationToken);
        }

        if (request?.Tags?.Any() == true)
        {
            var tagSet = request.Tags.ToHashSet();
            products.RemoveAll(p => p.Tags == null || !p.Tags.Any(t => tagSet.Contains(t.TagName)));
        }

        return products;
    }

    public Task EnrichAsync(List<ProductEntityInfo> products, CancellationToken cancellationToken) =>
        EnrichAsync(null!, products, cancellationToken);
}