using Airbnb.VerificationManagement.Domain.BoundedContexts.VerificationManagement.Aggregates;

namespace Airbnb.VerificationManagement.Domain.BoundedContexts.DocumentManagement.DomainDocumentManagement.Interfaces;

public interface IDomainDocumentRepository
{
    Task<DomainDocument<TDocumentData>> GetAsync<TDocumentData>(int id, CancellationToken cancellationToken = default)
        where TDocumentData : DocumentDataBase;

    Task<int> AddAsync<TDocumentData>(DomainDocument<TDocumentData> document, CancellationToken cancellationToken = default)
        where TDocumentData : DocumentDataBase;

    Task UpdateAsync<TDocumentData>(DomainDocument<TDocumentData> document, CancellationToken cancellationToken = default)
        where TDocumentData : DocumentDataBase;

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}