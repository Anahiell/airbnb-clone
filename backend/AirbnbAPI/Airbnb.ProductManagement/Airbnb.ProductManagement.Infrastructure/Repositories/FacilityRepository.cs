using Airbnb.Domain.BoundedContexts.ProductFacilityManagement.Facility.Aggregates;
using Airbnb.Domain.BoundedContexts.ProductFacilityManagement.Facility.Interfaces;
using Airbnb.Infrastructure.DataContext;
using Airbnb.SharedKernel.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.Infrastructure.Repositories;

public class FacilityRepository : IFacilityRepository
{
    private readonly AirbnbDbContext _context;

    public FacilityRepository(AirbnbDbContext context)
    {
        _context = context;
    }

    public async Task<int> AddAsync(Facility entity, CancellationToken cancellationToken = default)
    {
        var entry = await _context.Set<Facility>().AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entry.Entity.Id;
    }

    public async Task<Facility?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Facility>().FindAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<Facility>?> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Set<Facility>().ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(Facility entity, CancellationToken cancellationToken = default)
    {
        _context.Set<Facility>().Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Set<Facility>().FindAsync(id, cancellationToken);
        if (entity == null) return;
        _context.Set<Facility>().Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Facility>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Facility>()
            .Where(f => ids.Contains(f.Id))
            .ToListAsync(cancellationToken);
    }

    public async Task<Facility?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Facility>()
            .FirstOrDefaultAsync(f => f.Name == name, cancellationToken);
    }
}