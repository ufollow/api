using System.Threading.Tasks;
using ufollow.Domain;
using ufollow.Infrastructure.Database;

namespace ufollow.Infrastructure
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly ApiDbContext _dbContext;

        public UnitOfWork(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Complete()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
