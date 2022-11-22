using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Net.Http.Formatting;


namespace QuizAPiService.Entities
{
    public class ModelValidationActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var modelState = context.ModelState;
            if (!modelState.IsValid)
            {

                var errors = new List<string>();
                foreach (var state in modelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }

                var response = new { errors = errors };
                context.Result = new ContentResult()
                {
                    Content = response.ToString(),
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
                context.Result = new StatusCodeResult((int)HttpStatusCode.NotAcceptable);
            }
            base.OnActionExecuting(context);
        }
    }
}
