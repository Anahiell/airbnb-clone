using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Aggregates;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Interfaces;
using Airbnb.UserManagement.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.UserManagement.Infrastructure.Repositories;

public class UserRoleRepository : IUserRoleRepository
{
    private readonly ApplicationDbContext _context;

    public UserRoleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DomainUserRole?> GetByUserIdAndRoleIdAsync(int userId, int roleId, CancellationToken cancellationToken = default)
    {
        return await _context.UserRoles.FirstOrDefaultAsync(x => x.UserId == userId && x.RoleId == roleId, cancellationToken);
    }

    public async Task<IEnumerable<DomainUserRole>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        return await _context.UserRoles.Where(x => x.UserId == userId).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<DomainUserRole>> GetByRoleIdAsync(int roleId, CancellationToken cancellationToken = default)
    {
        return await _context.UserRoles.Where(x => x.RoleId == roleId).ToListAsync(cancellationToken);
    }

    public async Task<int> AddAsync(DomainUserRole userRole, CancellationToken cancellationToken = default)
    {
        var entityEntry = await _context.UserRoles.AddAsync(userRole, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entityEntry.Entity.Id;
    }
    
    public async Task UpdateAsync(DomainUserRole entity, CancellationToken cancellationToken = default)
    {
        _context.UserRoles.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveAsync(int userId, int roleId, CancellationToken cancellationToken = default)
    {
        var entity = await GetByUserIdAndRoleIdAsync(userId, roleId, cancellationToken);
        if (entity != null)
        {
            _context.UserRoles.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
    
    public async Task DeleteAllByUserIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        var entities = await _context.UserRoles
            .Where(ul => ul.UserId == userId)
            .ToListAsync(cancellationToken);

        if (entities.Count != 0)
        {
            _context.UserRoles.RemoveRange(entities);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}