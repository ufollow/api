namespace ufollow.Infrastructure.Mailing
{
    public class Mailbox
    {
        public Mailbox(string name, string address)
        {
            Name = name;
            Address = address;
        }

        public string Name { get; private set; }
        public string Address { get; private set; }
    }
}
