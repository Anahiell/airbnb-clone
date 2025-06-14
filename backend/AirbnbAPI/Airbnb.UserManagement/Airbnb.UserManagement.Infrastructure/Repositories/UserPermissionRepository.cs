using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Aggregates;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Interfaces;
using Airbnb.UserManagement.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.UserManagement.Infrastructure.Repositories;

public class UserPermissionRepository : IUserPermissionRepository
{
    private readonly ApplicationDbContext _context;

    public UserPermissionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DomainUserPermission?> GetByUserIdAndPermissionIdAsync(int userId, int permissionId, CancellationToken cancellationToken = default)
    {
        return await _context.UserPermissions.FirstOrDefaultAsync(
            x => x.UserId == userId && x.PermissionId == permissionId,
            cancellationToken
        );
    }
    
    public async Task UpdateAsync(DomainUserPermission userPermission, CancellationToken cancellationToken = default)
    {
        _context.UserPermissions.Update(userPermission);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<DomainUserPermission>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        return await _context.UserPermissions
            .Where(x => x.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<DomainUserPermission>> GetByPermissionIdAsync(int permissionId, CancellationToken cancellationToken = default)
    {
        return await _context.UserPermissions
            .Where(x => x.PermissionId == permissionId)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> AddAsync(DomainUserPermission userPermission, CancellationToken cancellationToken = default)
    {
        var entityEntry = await _context.UserPermissions.AddAsync(userPermission, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entityEntry.Entity.Id;
    }

    public async Task RemoveAsync(int userId, int permissionId, CancellationToken cancellationToken = default)
    {
        var entity = await GetByUserIdAndPermissionIdAsync(userId, permissionId, cancellationToken);
        if (entity != null)
        {
            _context.UserPermissions.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
    
    public async Task DeleteAllByUserIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        var entities = await _context.UserPermissions
            .Where(ul => ul.UserId == userId)
            .ToListAsync(cancellationToken);

        if (entities.Count != 0)
        {
            _context.UserPermissions.RemoveRange(entities);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}