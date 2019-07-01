using System.ComponentModel.DataAnnotations;

namespace ufollow.API.Models.Accounts
{
    public sealed class SignUpModel
    {
        [Required, MaxLength(80)]
        public string Name { get; set; }

        [Required, MaxLength(80), EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(8), MaxLength(20)]
        public string Password { get; set; }
    }
}
