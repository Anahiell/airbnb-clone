using Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Aggregates;
using Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Interfaces;
using Airbnb.UserManagement.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.UserManagement.Infrastructure.Repositories;

public class LanguageRepository : ILanguageRepository
    {
        private readonly ApplicationDbContext _context;

        public LanguageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DomainLanguage?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Languages.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task<IEnumerable<DomainLanguage>?> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Languages.ToListAsync(cancellationToken);
        }

        public async Task<int> AddAsync(DomainLanguage entity, CancellationToken cancellationToken = default)
        {
            await _context.Languages.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }

        public async Task UpdateAsync(DomainLanguage entity, CancellationToken cancellationToken = default)
        {
            _context.Languages.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await GetByIdAsync(id, cancellationToken);
            if (entity != null)
            {
                _context.Languages.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<DomainLanguage?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Languages
                .FirstOrDefaultAsync(l => l.Name == name, cancellationToken);
        }

        public async Task<IEnumerable<DomainLanguage>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default)
        {
            return await _context.Languages
                .Where(l => ids.Contains(l.Id))
                .ToListAsync(cancellationToken);
        }
    }