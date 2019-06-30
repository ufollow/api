using System;
using System.Security.Cryptography;
using System.Text;
using ufollow.Domain.ValueObjects;

namespace ufollow.Infrastructure.Security
{
    public sealed class Sha256Hash : ProtectedPassword
    {
        public Sha256Hash(string plainText)
        {
            Hash = Convert.ToBase64String(ToArray(plainText));
            Salt = Guid.NewGuid().ToString("N");
        }

        private byte[] ToArray(string plainText)
        {
            byte[] bytes = Encoding.UTF8.GetBytes($"{plainText}{Salt}");

            using (var sha = SHA256.Create())
            {
                return sha.ComputeHash(bytes);
            }
        }
    }
}
