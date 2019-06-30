using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ufollow.Domain.Entities;

namespace ufollow.Infrastructure.Database.Mapping
{
    public sealed class AccountMapping : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> account)
        {
            account.ToTable("accounts", ApiDbContext.Schema);

            account.HasKey(p => p.Id).HasName("pk_account");

            account.Property(p => p.Id).HasColumnName("id").UseNpgsqlSerialColumn();
            account.Property(p => p.BillingEmail).HasColumnName("email").HasMaxLength(80);
            account.Property(p => p.CreatedAt).HasColumnName("created_at").IsRequired();
            account.Property(p => p.DeactivatedAt).HasColumnName("deactivated_at");

            account.HasMany(p => p.Users)
                .WithOne(p => p.Account)
                .HasForeignKey(p => p.AccountId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_user__account");
        }
    }
}
