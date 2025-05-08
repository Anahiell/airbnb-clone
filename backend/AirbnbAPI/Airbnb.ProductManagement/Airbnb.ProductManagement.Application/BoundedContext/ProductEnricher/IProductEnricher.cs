using Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductEnricher;

public interface IProductEnricher
{
    Task EnrichAsync(List<ProductEntityInfo> products, CancellationToken cancellationToken);
}