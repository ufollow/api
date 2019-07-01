using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ufollow.API.Models.Accounts
{
    public sealed class TokenJson : IActionResult
    {
        public TokenJson(string token)
        {
            Token = token;
        }

        public string Token { get; set; }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            await new JsonResult(this).ExecuteResultAsync(context);
        }
    }
}
