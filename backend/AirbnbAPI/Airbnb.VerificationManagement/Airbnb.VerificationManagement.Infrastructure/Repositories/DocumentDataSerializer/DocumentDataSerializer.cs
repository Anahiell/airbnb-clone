using System.Text.Json;
using Airbnb.VerificationManagement.Domain.BoundedContexts.DocumentManagement.DriverLicenseManagement.Aggregates;
using Airbnb.VerificationManagement.Domain.BoundedContexts.DocumentManagement.DriverLicenseManagement.ValueObjects.DocumentType;
using Airbnb.VerificationManagement.Domain.BoundedContexts.DocumentManagement.PassportManagement.Aggregates;
using Airbnb.VerificationManagement.Domain.BoundedContexts.VerificationManagement.Aggregates;

namespace Airbnb.VerificationManagement.Domain.BoundedContexts.DocumentManagement.DomainDocumentManagement.ValueObjects.DocumentDataSerializer;

public class SystemTextJsonDocumentSerializer : IDocumentSerializer
{
    public string Serialize<TDocumentData>(TDocumentData data) where TDocumentData : DocumentDataBase
        => JsonSerializer.Serialize(data);

    public TDocumentData Deserialize<TDocumentData>(string json) where TDocumentData : DocumentDataBase
        => JsonSerializer.Deserialize<TDocumentData>(json)!;
}