using System.Threading.Tasks;

namespace ufollow.Infrastructure.Mailing
{
    public interface IMailer
    {
        Task Send(Envelope envelop);
    }
}
