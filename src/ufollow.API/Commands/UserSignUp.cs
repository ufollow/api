using System.Threading.Tasks;
using ufollow.API.Models.Accounts;
using ufollow.Domain.Entities;
using ufollow.Domain.Repositories;
using ufollow.Domain.ValueObjects;
using ufollow.Infrastructure.Security;

namespace ufollow.API.Commands
{
    public class UserSignUp
    {
        private readonly IUserRepository _userRepository;

        public UserSignUp(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool IsEmailAlreadyTaken { get; private set; }
        public User User { get; private set; }

        public async Task SignUp(SignUpModel dto)
        {
            IsEmailAlreadyTaken = await _userRepository.AnyUserWithEmail(dto.Email);

            if (IsEmailAlreadyTaken) return;

            User = new User(
                name: dto.Name,
                credentials: new Credentials(
                    email: dto.Email,
                    password: new Sha256Hash(dto.Password)
                )
            );

            User.Owns(new Account(billingEmail: dto.Email));

            _userRepository.Add(User);
        }
    }
}
