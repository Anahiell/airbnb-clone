using Airbnb.SharedKernel;

namespace Airbnb.EmailService.Domain.BoundedContexts.EmailManagement.Events;

public class EmailCreatedEvent : IDomainEvent
{
    public int AggregateId { get; }
    public string Recipient { get; }
    public string Subject { get; }
    public string HtmlMessage { get; }
    public int UserId { get; }
    public string? ConfirmationUrl { get; }

    public EmailCreatedEvent(int aggregateId, string recipient, string subject, string htmlMessage, int userId, string? confirmationUrl = null)
    {
        AggregateId = aggregateId;
        Recipient = recipient;
        Subject = subject;
        HtmlMessage = htmlMessage;
        UserId = userId;
        ConfirmationUrl = confirmationUrl;
    }
}