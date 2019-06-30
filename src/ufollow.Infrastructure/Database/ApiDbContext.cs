using Microsoft.EntityFrameworkCore;
using ufollow.Domain.Entities;
using ufollow.Infrastructure.Database.Mapping;

namespace ufollow.Infrastructure.Database
{
    public sealed class ApiDbContext : DbContext
    {
        public const string Schema = "marketing";

        public ApiDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountMapping());
            modelBuilder.ApplyConfiguration(new UserMapping());
        }

        [DbFunction("public.ci_ai")]
        public static string Normalize(string text) => text;
    }
}
