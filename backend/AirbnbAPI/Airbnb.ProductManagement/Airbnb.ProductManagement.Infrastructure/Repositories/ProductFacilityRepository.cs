using Airbnb.Domain.BoundedContexts.ProductFacilityManagement.ProductFacility.Aggregates;
using Airbnb.Domain.BoundedContexts.ProductFacilityManagement.ProductFacility.Interfaces;
using Airbnb.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.Infrastructure.Repositories;

public class ProductFacilityRepository : IProductFacilityRepository
{
    private readonly AirbnbDbContext _context;

    public ProductFacilityRepository(AirbnbDbContext context)
    {
        _context = context;
    }

    public async Task<ProductFacility?> GetByProductIdAndFacilityIdAsync(int productId, int facilityId, CancellationToken cancellationToken = default)
    {
        return await _context.ProductFacilities.FirstOrDefaultAsync(
            x => x.ProductId == productId && x.FacilityId == facilityId,
            cancellationToken
        );
    }

    public async Task<IEnumerable<ProductFacility>> GetByProductIdAsync(int productId, CancellationToken cancellationToken = default)
    {
        return await _context.ProductFacilities
            .Where(x => x.ProductId == productId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ProductFacility>> GetByFacilityIdAsync(int facilityId, CancellationToken cancellationToken = default)
    {
        return await _context.ProductFacilities
            .Where(x => x.FacilityId == facilityId)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> AddAsync(ProductFacility productFacility, CancellationToken cancellationToken = default)
    {
        var entry = await _context.ProductFacilities.AddAsync(productFacility, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entry.Entity.Id;
    }

    public async Task UpdateAsync(ProductFacility productFacility, CancellationToken cancellationToken = default)
    {
        _context.ProductFacilities.Update(productFacility);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveAsync(int productId, int facilityId, CancellationToken cancellationToken = default)
    {
        var entity = await GetByProductIdAndFacilityIdAsync(productId, facilityId, cancellationToken);
        if (entity != null)
        {
            _context.ProductFacilities.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task DeleteAllByProductIdAsync(int productId, CancellationToken cancellationToken = default)
    {
        var entities = await _context.ProductFacilities
            .Where(x => x.ProductId == productId)
            .ToListAsync(cancellationToken);

        if (entities.Count > 0)
        {
            _context.ProductFacilities.RemoveRange(entities);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}