using Airbnb.VerificationManagement.Domain.BoundedContexts.DocumentManagement.DriverLicenseManagement.ValueObjects.DocumentType;
using Airbnb.VerificationManagement.Domain.BoundedContexts.VerificationManagement.Aggregates;

namespace Airbnb.VerificationManagement.Application.BoundedContexts.DocumentManagement.Services.DocumentDispatcher;

public interface IDocumentFactory
{
    DomainDocument<TDocumentData> Create<TDocumentData>(int userId, string filePath)
        where TDocumentData : DocumentDataBase;
}