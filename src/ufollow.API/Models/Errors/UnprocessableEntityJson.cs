using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ufollow.API.Models.Errors
{
    public sealed class UnprocessableEntityJson : IActionResult
    {
        public UnprocessableEntityJson() { }

        public UnprocessableEntityJson(string error)
        {
            Error = error;
        }

        public string Error { get; set; }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var json = new JsonResult(this) { StatusCode = 422 };
            await json.ExecuteResultAsync(context);
        }
    }

}
