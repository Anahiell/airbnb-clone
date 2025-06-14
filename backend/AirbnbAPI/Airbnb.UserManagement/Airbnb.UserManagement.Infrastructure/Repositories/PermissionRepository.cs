using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Aggregates;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Interfaces;
using Airbnb.UserManagement.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.UserManagement.Infrastructure.Repositories;

public class PermissionRepository : IPermissionRepository
{
    private readonly ApplicationDbContext _context;

    public PermissionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DomainPermission?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Permissions.FindAsync(new object?[] { id }, cancellationToken);
    }

    public async Task<IEnumerable<DomainPermission>?> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Permissions.ToListAsync(cancellationToken);
    }

    public async Task<int> AddAsync(DomainPermission entity, CancellationToken cancellationToken = default)
    {
        await _context.Permissions.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }

    public async Task UpdateAsync(DomainPermission entity, CancellationToken cancellationToken = default)
    {
        _context.Permissions.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity != null)
        {
            _context.Permissions.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<DomainPermission?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.Permissions.FirstOrDefaultAsync(p => p.Permission == name, cancellationToken);
    }

    public async Task<IEnumerable<DomainPermission>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default)
    {
        return await _context.Permissions.Where(p => ids.Contains(p.Id)).ToListAsync(cancellationToken);
    }
}