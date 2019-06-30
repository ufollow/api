namespace ufollow.Domain.ValueObjects
{
    public class ProtectedPassword
    {
        protected ProtectedPassword() { }

        public string Hash { get; protected set; }
        public string Salt { get; protected set; }

        public bool Matches(ProtectedPassword password)
        {
            return Hash == password.Hash && Salt == password.Salt;
        }
    }
}
