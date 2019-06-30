using System;
using System.Collections.Generic;

namespace ufollow.Domain.Entities
{
    public class Account
    {
        public Account(string billingEmail)
        {
            BillingEmail = BillingEmail;
            CreatedAt = DateTime.UtcNow;
        }

        public long Id { get; private set; }
        public string BillingEmail { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? DeactivatedAt { get; private set; }
        public IEnumerable<User> Users { get; private set; }
    }
}
