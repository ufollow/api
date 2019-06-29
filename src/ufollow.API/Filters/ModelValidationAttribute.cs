using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;
using ufollow.API.Models.Errors;

namespace ufollow.API.Filters
{
    public sealed class ModelValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.ActionArguments.Any(arg => arg.Value == null))
            {
                filterContext.Result = new BadRequestJson("Request body cannot be blank.");
            }
            else if (!filterContext.ModelState.IsValid)
            {
                var errors = filterContext.ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => new ModelErrorMessage(e))
                    .ToList();

                filterContext.Result = new BadRequestJson(errors);
            }
        }
    }
}
