using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ufollow.Domain.Entities;

namespace ufollow.Infrastructure.Database.Mapping
{
    public sealed class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> user)
        {
            user.ToTable("users", ApiDbContext.Schema);

            user.HasKey(p => new { p.AccountId, p.Id }).HasName("pk_user");

            user.Property(p => p.AccountId).HasColumnName("account_id");
            user.Property(p => p.Id).HasColumnName("id").UseNpgsqlSerialColumn();
            user.Property(p => p.Name).HasColumnName("name").HasMaxLength(80).IsRequired();
            user.Property(p => p.PasswordLastChange).HasColumnName("password_last_change");
            user.Property(p => p.IsOwner).HasColumnName("is_owner").IsRequired();
            user.Property(p => p.CreatedAt).HasColumnName("created_at").IsRequired();
            user.Property(p => p.DeactivatedAt).HasColumnName("deactivated_at");

            user.OwnsOne(p => p.Credentials, credentials =>
            {
                credentials.HasIndex(p => p.Email).HasName("unq_user_email").IsUnique();

                credentials.Property(p => p.Email).HasColumnName("email").HasMaxLength(80).IsRequired();
                credentials.Property(p => p.Password.Hash).HasColumnName("password").HasMaxLength(255);
                credentials.Property(p => p.Password.Salt).HasColumnName("salt").HasMaxLength(50).IsRequired();

                credentials.OwnsOne(p => p.Password, password =>
                {
                    password.Property(p => p.Hash).HasColumnName("password").HasMaxLength(255);
                    password.Property(p => p.Salt).HasColumnName("salt").HasMaxLength(50).IsRequired();
                });
            });
        }
    }
}
