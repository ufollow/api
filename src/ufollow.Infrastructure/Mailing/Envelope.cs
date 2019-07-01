namespace ufollow.Infrastructure.Mailing
{
    public sealed class Envelope
    {
        public Envelope(Mailbox sender, Mailbox recipient, MailMessage message)
        {
            Sender = sender;
            Recipient = recipient;
            Message = message;
        }

        public Mailbox Sender { get; private set; }
        public Mailbox Recipient { get; private set; }
        public MailMessage Message { get; private set; }
    }
}
