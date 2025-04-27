using Airbnb.SharedKernel;
using Airbnb.EmailService.Domain.BoundedContexts.EmailManagement.Events;

namespace Airbnb.Domain.BoundedContexts.EmailManagement.Aggregates
{
    public class DomainEmail : AggregateRoot
    {
        public string Recipient { get; private set; }
        public string Subject { get; private set; }
        public string HtmlMessage { get; private set; }
        public bool IsSent { get; private set; }
        public DateTime SentAt { get; private set; }
        public int UserId { get; private set; }
        public string? ConfirmationUrl { get; private set; }
        public bool IsConfirmed { get; private set; }

        public DomainEmail() { }

        public DomainEmail(string recipient, string subject, string htmlMessage, int userId, string? confirmationUrl = null)
        {
            Recipient = recipient;
            Subject = subject;
            HtmlMessage = htmlMessage;
            UserId = userId;
            ConfirmationUrl = confirmationUrl;
            IsSent = false;
            IsConfirmed = false;
            SentAt = DateTime.MinValue;

            RaiseEvent(new EmailCreatedEvent(Id, recipient, subject, htmlMessage, userId, confirmationUrl));
        }

        #region Aggregate Methods

        public void SendEmail()
        {
            if (IsSent)
                throw new InvalidOperationException("Email has already been sent.");

            IsSent = true;
            SentAt = DateTime.UtcNow;

            RaiseEvent(new EmailSentEvent(Id, Recipient, Subject, HtmlMessage, UserId));
        }

        public void ConfirmEmail()
        {
            if (string.IsNullOrEmpty(ConfirmationUrl))
                throw new InvalidOperationException("No confirmation URL available.");

            IsConfirmed = true;

            RaiseEvent(new EmailConfirmedEvent(Id, UserId));
        }

        #endregion

        #region Event Handling

        protected override void When(IDomainEvent @event)
        {
            switch (@event)
            {
                case EmailCreatedEvent e:
                    OnEmailCreatedEvent(e);
                    break;
                case EmailSentEvent e:
                    OnEmailSentEvent(e);
                    break;
                case EmailConfirmedEvent e:
                    OnEmailConfirmedEvent(e);
                    break;
            }
        }

        private void OnEmailCreatedEvent(EmailCreatedEvent @event)
        {
            Id = @event.AggregateId;
            Recipient = @event.Recipient;
            Subject = @event.Subject;
            HtmlMessage = @event.HtmlMessage;
            UserId = @event.UserId;
            ConfirmationUrl = @event.ConfirmationUrl;
            IsSent = false;
            IsConfirmed = false;
            SentAt = DateTime.MinValue;
        }

        private void OnEmailSentEvent(EmailSentEvent @event)
        {
            Id = @event.AggregateId;
            IsSent = true;
            SentAt = @event.SentAt;
        }

        private void OnEmailConfirmedEvent(EmailConfirmedEvent @event)
        {
            Id = @event.AggregateId;
            IsConfirmed = true;
        }

        #endregion
    }
}
