namespace ufollow.Infrastructure.Mailing
{
    public class MailMessage
    {
        public MailMessage(string subject, HtmlMessage content)
        {
            Subject = subject;
            Content = content;
        }

        public string Subject { get; private set; }
        public HtmlMessage Content { get; private set; }
    }
}
