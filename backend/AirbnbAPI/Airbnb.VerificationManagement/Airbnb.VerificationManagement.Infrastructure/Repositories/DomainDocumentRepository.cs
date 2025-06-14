using Airbnb.VerificationManagement.Domain.BoundedContexts.DocumentManagement.DomainDocumentManagement.Interfaces;
using Airbnb.VerificationManagement.Domain.BoundedContexts.DocumentManagement.DomainDocumentManagement.ValueObjects.DocumentDataSerializer;
using Airbnb.VerificationManagement.Domain.BoundedContexts.VerificationManagement.Aggregates;
using Airbnb.VerificationManagement.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.VerificationManagement.Infrastructure.Repositories;

public class DomainDocumentRepository : IDomainDocumentRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IDocumentSerializer _serializer;

    public DomainDocumentRepository(ApplicationDbContext context, IDocumentSerializer serializer)
    {
        _context = context;
        _serializer = serializer;
    }

    public async Task<DomainDocument<TDocumentData>> GetAsync<TDocumentData>(int id, CancellationToken cancellationToken = default)
        where TDocumentData : DocumentDataBase
    {
        var entity = await _context.Documents
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (entity == null)
            throw new KeyNotFoundException($"Document with id {id} not found");

        var data = _serializer.Deserialize<TDocumentData>(entity.DataJson);

        return new DomainDocument<TDocumentData>(
            id: entity.Id,
            userId: entity.UserId,
            filePath: entity.FilePath,
            uploadedAt: entity.UploadedAt,
            documentData: data
        );
    }

    public async Task<int> AddAsync<TDocumentData>(DomainDocument<TDocumentData> document, CancellationToken cancellationToken = default)
        where TDocumentData : DocumentDataBase
    {
        var dataJson = _serializer.Serialize(document.DocumentData);
        
        var entity = new DomainDocument<TDocumentData>(
            id: document.Id,
            userId: document.UserId,
            filePath: document.FilePath,
            uploadedAt: document.UploadedAt,
            documentData: dataJson
        );

        _context.Documents.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }

    public async Task UpdateAsync<TDocumentData>(DomainDocument<TDocumentData> document, CancellationToken cancellationToken = default)
        where TDocumentData : DocumentDataBase
    {
        var entity = await _context.Documents.FirstOrDefaultAsync(x => x.Id == document.Id, cancellationToken);
        if (entity == null)
            throw new KeyNotFoundException($"Document with id {document.Id} not found");

        entity.Update(
            userId: document.UserId,
            filePath: document.FilePath,
            uploadedAt: document.UploadedAt,
            dataJson: _serializer.Serialize(document.DocumentData)
        );

        _context.Documents.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Documents.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (entity != null)
        {
            _context.Documents.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}