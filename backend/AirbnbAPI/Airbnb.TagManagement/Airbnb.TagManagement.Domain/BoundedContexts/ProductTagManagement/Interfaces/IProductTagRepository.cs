using Airbnb.TagsManagement.Domain.BoundedContexts.ProductTagManagement.Aggregates;

namespace Airbnb.TagsManagement.Domain.BoundedContexts.ProductTagManagement.Interfaces;

public interface IProductTagRepository
{
    Task<List<(int TagId, string TagName)>> GetTagsByProductIdAsync(int productId, CancellationToken cancellationToken = default);
    Task<ProductTag?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<int> AddAsync(ProductTag productTag, CancellationToken cancellationToken = default);
    Task UpdateAsync(ProductTag productTag, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}