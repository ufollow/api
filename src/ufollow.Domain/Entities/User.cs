using System;
using ufollow.Domain.ValueObjects;

namespace ufollow.Domain.Entities
{
    public class User
    {
        private User() { }

        public User(string name, Credentials credentials)
        {
            CreatedAt = DateTime.UtcNow;
            PasswordLastChange = CreatedAt;
        }

        public long Id { get; private set; }
        public long AccountId { get; private set; }
        public string Name { get; private set; }
        public Credentials Credentials { get; private set; }
        public bool IsOwner { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? PasswordLastChange { get; private set; }
        public DateTime? DeactivatedAt { get; private set; }
        public Account Account { get; private set; }

        public void ChangePassword(ProtectedPassword password)
        {
            Credentials = new Credentials(
                email: Credentials.Email,
                password: password
            );

            PasswordLastChange = DateTime.UtcNow;
        }

        public void CollaborateTo(Account account)
        {
            Account = account;
            IsOwner = false;
        }

        public void Owns(Account account)
        {
            Account = account;
            IsOwner = true;
        }
    }
}
