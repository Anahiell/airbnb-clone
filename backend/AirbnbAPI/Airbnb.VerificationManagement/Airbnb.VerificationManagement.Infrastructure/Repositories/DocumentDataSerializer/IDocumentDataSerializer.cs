using Airbnb.VerificationManagement.Domain.BoundedContexts.DocumentManagement.DriverLicenseManagement.ValueObjects.DocumentType;
using Airbnb.VerificationManagement.Domain.BoundedContexts.VerificationManagement.Aggregates;

namespace Airbnb.VerificationManagement.Domain.BoundedContexts.DocumentManagement.DomainDocumentManagement.ValueObjects.DocumentDataSerializer;

public interface IDocumentSerializer
{
    string Serialize<TDocumentData>(TDocumentData data) where TDocumentData : DocumentDataBase;
    TDocumentData Deserialize<TDocumentData>(string json) where TDocumentData : DocumentDataBase;
}