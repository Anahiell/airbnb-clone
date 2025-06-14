using Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Aggregates;
using Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Interfaces;
using Airbnb.UserManagement.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.UserManagement.Infrastructure.Repositories;

public class UserLanguageRepository : IUserLanguageRepository
{
    private readonly ApplicationDbContext _context;

    public UserLanguageRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DomainUserLanguage?> GetByUserIdAndLanguageIdAsync(int userId, int languageId, CancellationToken cancellationToken = default)
        => await _context.UserLanguages
            .FirstOrDefaultAsync(ul => ul.UserId == userId && ul.LanguageId == languageId, cancellationToken);

    public async Task<IEnumerable<DomainUserLanguage>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default)
        => await _context.UserLanguages
            .Where(ul => ul.UserId == userId)
            .ToListAsync(cancellationToken);

    public async Task<IEnumerable<DomainUserLanguage>> GetByLanguageIdAsync(int languageId, CancellationToken cancellationToken = default)
        => await _context.UserLanguages
            .Where(ul => ul.LanguageId == languageId)
            .ToListAsync(cancellationToken);

    public async Task<int> AddAsync(DomainUserLanguage userLanguage, CancellationToken cancellationToken = default)
    {
        await _context.UserLanguages.AddAsync(userLanguage, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return userLanguage.Id;
    }

    public async Task UpdateAsync(DomainUserLanguage entity, CancellationToken cancellationToken = default)
    {
        _context.UserLanguages.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveAsync(int userId, int languageId, CancellationToken cancellationToken = default)
    {
        var entity = await GetByUserIdAndLanguageIdAsync(userId, languageId, cancellationToken);
        if (entity is null)
            return;

        _context.UserLanguages.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task DeleteAllByUserIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        var entities = await _context.UserLanguages
            .Where(ul => ul.UserId == userId)
            .ToListAsync(cancellationToken);

        if (entities.Count != 0)
        {
            _context.UserLanguages.RemoveRange(entities);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
