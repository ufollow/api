namespace ufollow.Domain.ValueObjects
{
    public sealed class Credentials
    {
        public Credentials(string email, ProtectedPassword password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; }
        public ProtectedPassword Password { get; }

        public bool Matches(Credentials credentials)
        {
            return credentials.Email == Email && Password.Matches(credentials.Password);
        }
    }
}
