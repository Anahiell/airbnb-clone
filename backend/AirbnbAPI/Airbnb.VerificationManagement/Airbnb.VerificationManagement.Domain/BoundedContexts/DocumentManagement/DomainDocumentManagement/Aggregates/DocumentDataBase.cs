using Airbnb.SharedKernel;
using Airbnb.VerificationManagement.Domain.BoundedContexts.DocumentManagement.DriverLicenseManagement.ValueObjects.DocumentType;

namespace Airbnb.VerificationManagement.Domain.BoundedContexts.VerificationManagement.Aggregates;

public abstract class DocumentDataBase : AggregateRoot
{
    public int Id { get; protected set; }
    public int UserId { get; protected set; }
    public string FilePath { get; protected set; }
    public DateTime UploadedAt { get; protected set; }
    public string DataJson { get; protected set; }
    public virtual DocumentType Type { get; protected set; }
    
    public void Update(int userId, string filePath, DateTime uploadedAt, string dataJson)
    {
        UserId = userId;
        FilePath = filePath;
        UploadedAt = uploadedAt;
        DataJson = dataJson;
    }
    
    protected override void When(IDomainEvent @event)
    {

    }
}