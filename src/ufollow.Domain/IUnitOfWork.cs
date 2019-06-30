using System.Threading.Tasks;

namespace ufollow.Domain
{
    public interface IUnitOfWork
    {
        Task Complete();
    }
}
