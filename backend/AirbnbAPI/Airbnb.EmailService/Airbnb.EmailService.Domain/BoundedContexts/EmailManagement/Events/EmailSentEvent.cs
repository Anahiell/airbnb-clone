using Airbnb.SharedKernel;

namespace Airbnb.EmailService.Domain.BoundedContexts.EmailManagement.Events;

public class EmailSentEvent : IDomainEvent
{
    public int AggregateId { get; }
    public string Recipient { get; }
    public string Subject { get; }
    public string HtmlMessage { get; }
    public int UserId { get; }
    public DateTime SentAt { get; }

    public EmailSentEvent(int aggregateId, string recipient, string subject, string htmlMessage, int userId)
    {
        AggregateId = aggregateId;
        Recipient = recipient;
        Subject = subject;
        HtmlMessage = htmlMessage;
        UserId = userId;
        SentAt = DateTime.UtcNow;
    }
}