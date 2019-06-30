using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ufollow.Domain.Entities;
using ufollow.Domain.Repositories;

namespace ufollow.Infrastructure.Database.Repositories
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly ApiDbContext _dbContext;

        public UserRepository(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(User user)
        {
            _dbContext.Users.Add(user);
        }

        public async Task<bool> AnyUserWithEmail(string email)
        {
            return await _dbContext.Users
                .Where(user => user.Credentials.Email == email)
                .AnyAsync();
        }

        public async Task<User> FindByEmail(string email)
        {
            return await _dbContext.Users
                .Where(user => user.Credentials.Email == email)
                .SingleOrDefaultAsync();
        }

        public async Task<User> FindById(long id)
        {
            return await _dbContext.Users
                .Where(user => user.Id == id)
                .SingleOrDefaultAsync();
        }

        public void Remove(User user)
        {
            _dbContext.Users.Remove(user);
        }
    }
}
