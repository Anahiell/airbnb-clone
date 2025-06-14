using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Interfaces;
using Airbnb.UserManagement.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.UserManagement.Infrastructure.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly ApplicationDbContext _context;

    public RoleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DomainRole?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Roles.FindAsync(new object?[] { id }, cancellationToken);
    }

    public async Task<IEnumerable<DomainRole>?> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Roles.ToListAsync(cancellationToken);
    }

    public async Task<int> AddAsync(DomainRole entity, CancellationToken cancellationToken = default)
    {
        await _context.Roles.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }

    public async Task UpdateAsync(DomainRole entity, CancellationToken cancellationToken = default)
    {
        _context.Roles.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity != null)
        {
            _context.Roles.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<DomainRole?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.Roles.FirstOrDefaultAsync(r => r.Name == name, cancellationToken);
    }

    public async Task<IEnumerable<DomainRole>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default)
    {
        return await _context.Roles.Where(r => ids.Contains(r.Id)).ToListAsync(cancellationToken);
    }
}