using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ufollow.API.Models.Errors
{
    public sealed class BadRequestJson : IActionResult
    {
        public BadRequestJson() { }

        public BadRequestJson(params string[] errors)
        {
            Errors = errors;
        }

        public BadRequestJson(IEnumerable<ModelErrorMessage> modelErrorMessages)
        {
            Errors = modelErrorMessages.Select(modelErrorMessage => modelErrorMessage.ToString());
        }

        public IEnumerable<string> Errors { get; set; }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var json = new JsonResult(this) { StatusCode = 400 };
            await json.ExecuteResultAsync(context);
        }
    }
}
