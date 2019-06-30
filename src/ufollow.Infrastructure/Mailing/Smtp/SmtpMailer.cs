using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace ufollow.Infrastructure.Mailing.Smtp
{
    public sealed class SmtpMailer : IMailer
    {
        private readonly SmtpOptions options;

        public SmtpMailer(SmtpOptions options)
        {
            this.options = options;
        }

        public async Task Send(Envelope envelope)
        {
            var body = new BodyBuilder();
            body.HtmlBody = envelope.Message.Minified();

            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(envelope.SenderName, envelope.SenderEmail));
            mimeMessage.To.Add(new MailboxAddress(envelope.RecipientName, envelope.RecipientEmail));
            mimeMessage.Subject = envelope.Subject;
            mimeMessage.Body = body.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect(options.Host, options.Port, options.UseSsl);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(options.Username, options.Password);

                await client.SendAsync(mimeMessage);

                client.Disconnect(true);
            }
        }

    }
}
