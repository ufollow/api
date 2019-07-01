using System.Threading.Tasks;
using ufollow.Domain.Entities;

namespace ufollow.Domain.Repositories
{
    public interface IUserRepository
    {
        void Add(User user);
        Task<User> FindById(long id);
        Task<User> FindByEmail(string email);
        Task<bool> HasUserWithEmail(string email);
        void Remove(User user);
    }
}
