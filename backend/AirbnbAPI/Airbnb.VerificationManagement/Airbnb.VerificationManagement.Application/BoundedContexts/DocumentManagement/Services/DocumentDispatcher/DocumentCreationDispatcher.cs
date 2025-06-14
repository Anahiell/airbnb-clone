using Airbnb.VerificationManagement.Domain.BoundedContexts.DocumentManagement.DriverLicenseManagement.Aggregates;
using Airbnb.VerificationManagement.Domain.BoundedContexts.DocumentManagement.DriverLicenseManagement.ValueObjects.DocumentType;
using Airbnb.VerificationManagement.Domain.BoundedContexts.DocumentManagement.PassportManagement.Aggregates;
using Airbnb.VerificationManagement.Domain.BoundedContexts.VerificationManagement.Aggregates;

namespace Airbnb.VerificationManagement.Application.BoundedContexts.DocumentManagement.Services.DocumentDispatcher;

public class DocumentFactory : IDocumentFactory
{
    public DomainDocument<TDocumentData> Create<TDocumentData>(int userId, string filePath)
        where TDocumentData : DocumentDataBase
    {
        var documentTypeEnum = GetDocumentTypeEnum<TDocumentData>();
        var documentType = new DocumentType(documentTypeEnum);
        return new DomainDocument<TDocumentData>(userId, documentType, filePath);
    }

    private static DocumentTypeEnum GetDocumentTypeEnum<TDocumentData>()
    {
        return typeof(TDocumentData) switch
        {
            var t when t == typeof(PassportDocument) => DocumentTypeEnum.Passport,
            var t when t == typeof(DriverLicenseDocument) => DocumentTypeEnum.DriverLicense,
            _ => throw new InvalidOperationException($"Unsupported document type: {typeof(TDocumentData).Name}")
        };
    }
}