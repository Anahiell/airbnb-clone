namespace Airbnb.Domain.BoundedContexts.ProductManagement.Interfaces;


public interface IProductRepository
{
    Task<int> CreateProductAsync(DomainProduct propertyEntity, CancellationToken cancellationToken = default);
    Task<DomainProduct> GetProductByIdAsync(int productId);
    Task<IEnumerable<DomainProduct>> GetAllProductsAsync();
}
