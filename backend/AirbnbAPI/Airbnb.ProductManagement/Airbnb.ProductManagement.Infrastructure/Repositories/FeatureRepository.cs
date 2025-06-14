using Airbnb.Domain.BoundedContexts.ProductFeatureManagement.Feature.Aggregates;
using Airbnb.Domain.BoundedContexts.ProductFeatureManagement.Feature.Interfaces;
using Airbnb.Infrastructure.DataContext;
using Airbnb.SharedKernel.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.Infrastructure.Repositories;

public class FeatureRepository : IFeatureRepository
{
    private readonly AirbnbDbContext _context;

    public FeatureRepository(AirbnbDbContext context)
    {
        _context = context;
    }

    public async Task<int> AddAsync(Feature entity, CancellationToken cancellationToken = default)
    {
        var entry = await _context.Set<Feature>().AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entry.Entity.Id;
    }

    public async Task<Feature?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Feature>().FindAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<Feature>?> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Set<Feature>().ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(Feature entity, CancellationToken cancellationToken = default)
    {
        _context.Set<Feature>().Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Set<Feature>().FindAsync(id, cancellationToken);
        if (entity == null) return;
        _context.Set<Feature>().Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<IEnumerable<Feature>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Feature>()
            .Where(f => ids.Contains(f.Id))
            .ToListAsync(cancellationToken);
    }

    public async Task<Feature?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Feature>()
            .FirstOrDefaultAsync(f => f.Name == name, cancellationToken);
    }
}