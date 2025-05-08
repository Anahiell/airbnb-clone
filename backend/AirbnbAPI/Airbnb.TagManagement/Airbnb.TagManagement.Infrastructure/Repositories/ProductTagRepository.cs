using Airbnb.TagsManagement.Domain.BoundedContexts.ProductTagManagement.Aggregates;
using Airbnb.TagsManagement.Domain.BoundedContexts.ProductTagManagement.Interfaces;
using Airbnb.TagsManagement.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

public class ProductTagRepository : IProductTagRepository
{
    private readonly AirbnbDbContext _context;

    public ProductTagRepository(AirbnbDbContext context)
    {
        _context = context;
    }

    public async Task<List<(int TagId, string TagName)>> GetTagsByProductIdAsync(int productId, CancellationToken cancellationToken = default)
    {
        return await _context.ProductTag
            .Where(pt => pt.ProductId == productId)
            .Join(
                _context.DomainTag,
                pt => pt.TagId,
                tag => tag.Id,
                (pt, tag) => new { tag.Id, tag.Name }
            )
            .Select(t => new ValueTuple<int, string>(t.Id, t.Name))
            .ToListAsync(cancellationToken);
    }

    public async Task<ProductTag?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.ProductTag.FindAsync([id], cancellationToken);
    }

    public async Task<int> AddAsync(ProductTag productTag, CancellationToken cancellationToken = default)
    {
        _context.ProductTag.Add(productTag);
        await _context.SaveChangesAsync(cancellationToken);
        return productTag.Id;
    }

    public async Task UpdateAsync(ProductTag productTag, CancellationToken cancellationToken = default)
    {
        _context.ProductTag.Update(productTag);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.ProductTag.FindAsync([id], cancellationToken);
        if (entity is null) return;

        _context.ProductTag.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}