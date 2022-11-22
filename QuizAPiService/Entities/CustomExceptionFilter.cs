using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;


namespace QuizAPiService.Entities
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<CustomExceptionFilter> _logger;

        public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            string errorMessage = context.Exception.Message;
            var statusCode = HttpStatusCode.InternalServerError;

            if (context.Exception is NotFoundException)
            {
                statusCode = HttpStatusCode.NotFound;
                _logger.LogWarning(new EventId(0), context.Exception, errorMessage);
            }
            else if (context.Exception is UnauthorizedAccessException)
            {
                statusCode = HttpStatusCode.Unauthorized;
                _logger.LogError(new EventId(0), context.Exception, errorMessage);
            }
            else
            {
                statusCode = HttpStatusCode.InternalServerError;
                _logger.LogError(new EventId(0), context.Exception, errorMessage);

            }

            // Set the result
            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)statusCode;
            context.Result = new JsonResult(new
            {
                error = new[] { context.Exception.Message },
                stackTrace = context.Exception.StackTrace,
                source = context.Exception.Source,
                ExceptionType = context.Exception.GetType().FullName,
            });
        }

    }
}