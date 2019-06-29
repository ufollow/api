namespace ufollow.Infrastructure.Mailing
{
    public sealed class Envelope
    {
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string RecipientName { get; set; }
        public string RecipientEmail { get; set; }
        public string Subject { get; set; }
        public HtmlMessage Message { get; set; }
    }
}
