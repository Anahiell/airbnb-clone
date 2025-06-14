using System.Linq.Expressions;
using Airbnb.Domain.BoundedContexts.ProductRulesManagement.Aggregates;
using Airbnb.Domain.BoundedContexts.ProductRulesManagement.Interfaces;
using Airbnb.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.Infrastructure.Repositories;

public class RuleRepository : IRuleRepository
{
    private readonly AirbnbDbContext _context;

    public RuleRepository(AirbnbDbContext context)
    {
        _context = context;
    }

    public async Task<int> AddAsync(Rule entity, CancellationToken cancellationToken = default)
    {
        var entry = await _context.Set<Rule>().AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entry.Entity.Id;
    }

    public async Task<Rule?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Rule>().FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<IEnumerable<Rule>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Set<Rule>().ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(Rule entity, CancellationToken cancellationToken = default)
    {
        _context.Set<Rule>().Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Set<Rule>().FindAsync(new object[] { id }, cancellationToken);
        if (entity == null) return;
        _context.Set<Rule>().Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
    
    public Task DeleteAsync(Expression<Func<Rule, bool>> predicate, CancellationToken cancellationToken = default)
    {
        _context.Set<Rule>().RemoveRange(_context.Set<Rule>().Where(predicate));
        return _context.SaveChangesAsync(cancellationToken);
    }
}