using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ufollow.API.Authorization;
using ufollow.API.Models.Accounts;
using ufollow.API.Commands;
using ufollow.Domain;
using ufollow.Domain.Repositories;

namespace ufollow.API.Controllers
{
    public sealed class SubscriptionsController : Controller
    {
        private readonly ApiTokenOptions _apiTokenOptions;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SubscriptionsController(ApiTokenOptions apiTokenOptions,
            IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _apiTokenOptions = apiTokenOptions;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpPost, Route("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel dto)
        {
            var userSignUp = new UserSignUp(_userRepository);

            await userSignUp.SignUp(dto);

            if (userSignUp.IsEmailAlreadyTaken)
            {
                return new EmailAlreadyTakenError();
            }

            await _unitOfWork.Complete();

            var token = new ApiToken(_apiTokenOptions).Generate(userSignUp.User);

            return new TokenJson(token);
        }
    }
}
