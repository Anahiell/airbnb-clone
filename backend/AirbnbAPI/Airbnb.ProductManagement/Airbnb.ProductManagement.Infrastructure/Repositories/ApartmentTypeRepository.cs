using Airbnb.Domain.BoundedContexts.PropertyTypeManagement.Aggregates;
using Airbnb.Infrastructure.DataContext;
using Airbnb.SharedKernel.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.Infrastructure.Repositories;

public class ApartmentTypeRepository : IRepository<ApartmentType>
{
    private readonly AirbnbDbContext _context;

    public ApartmentTypeRepository(AirbnbDbContext context)
    {
        _context = context;
    }

    public async Task<int> AddAsync(ApartmentType apartmentType, CancellationToken cancellationToken = default)
    {
        var entityEntry = await _context.Set<ApartmentType>().AddAsync(apartmentType, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entityEntry.Entity.Id;
    }

    public async Task<ApartmentType?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<ApartmentType>().FindAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<ApartmentType>?> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Set<ApartmentType>().ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(ApartmentType entity, CancellationToken cancellationToken = default)
    {
        _context.Set<ApartmentType>().Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Set<ApartmentType>().FindAsync(id, cancellationToken);
        if (entity == null) return;
        _context.Set<ApartmentType>().Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}