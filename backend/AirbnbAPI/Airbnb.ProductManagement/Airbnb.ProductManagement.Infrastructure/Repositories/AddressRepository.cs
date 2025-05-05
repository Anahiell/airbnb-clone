using Airbnb.Domain.BoundedContexts.AddressManagement.Aggregates;
using Airbnb.Infrastructure.DataContext;
using Airbnb.SharedKernel.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.Infrastructure.Repositories;

public class AddressRepository : IRepository<AddressLegal>
{
    private readonly AirbnbDbContext _context;

    public AddressRepository(AirbnbDbContext context)
    {
        _context = context;
    }

    public async Task<int> AddAsync(AddressLegal address, CancellationToken cancellationToken = default)
    {
        var entityEntry = await _context.Set<AddressLegal>().AddAsync(address, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entityEntry.Entity.Id;
    }

    public async Task<AddressLegal?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<AddressLegal>().FindAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<AddressLegal>?> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Set<AddressLegal>().ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(AddressLegal entity, CancellationToken cancellationToken = default)
    {
        _context.Set<AddressLegal>().Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Set<AddressLegal>().FindAsync(id, cancellationToken);
        if (entity == null) return;
        _context.Set<AddressLegal>().Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}