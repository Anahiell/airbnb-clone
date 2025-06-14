using Airbnb.Domain.BoundedContexts.ProductFeatureManagement.ProductFeature.Aggregates;
using Airbnb.Domain.BoundedContexts.ProductFeatureManagement.ProductFeature.Interfaces;
using Airbnb.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.Infrastructure.Repositories;

public class ProductFeatureRepository : IProductFeatureRepository
{
    private readonly AirbnbDbContext _context;

    public ProductFeatureRepository(AirbnbDbContext context)
    {
        _context = context;
    }

    public async Task<ProductFeature?> GetByProductIdAndFeatureIdAsync(int productId, int featureId, CancellationToken cancellationToken = default)
    {
        return await _context.ProductFeatures.FirstOrDefaultAsync(
            x => x.ProductId == productId && x.FeatureId == featureId,
            cancellationToken
        );
    }

    public async Task<IEnumerable<ProductFeature>> GetByProductIdAsync(int productId, CancellationToken cancellationToken = default)
    {
        return await _context.ProductFeatures
            .Where(x => x.ProductId == productId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ProductFeature>> GetByFeatureIdAsync(int featureId, CancellationToken cancellationToken = default)
    {
        return await _context.ProductFeatures
            .Where(x => x.FeatureId == featureId)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> AddAsync(ProductFeature productFeature, CancellationToken cancellationToken = default)
    {
        var entry = await _context.ProductFeatures.AddAsync(productFeature, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entry.Entity.Id;
    }

    public async Task UpdateAsync(ProductFeature productFeature, CancellationToken cancellationToken = default)
    {
        _context.ProductFeatures.Update(productFeature);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveAsync(int productId, int featureId, CancellationToken cancellationToken = default)
    {
        var entity = await GetByProductIdAndFeatureIdAsync(productId, featureId, cancellationToken);
        if (entity != null)
        {
            _context.ProductFeatures.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task DeleteAllByProductIdAsync(int productId, CancellationToken cancellationToken = default)
    {
        var entities = await _context.ProductFeatures
            .Where(x => x.ProductId == productId)
            .ToListAsync(cancellationToken);

        if (entities.Count > 0)
        {
            _context.ProductFeatures.RemoveRange(entities);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}