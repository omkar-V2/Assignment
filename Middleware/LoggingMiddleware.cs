using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net;
using System.Text.Json;
using Newtonsoft.Json;

namespace CCMPreparation.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate requestDelegate, ILogger<LoggingMiddleware> logger)
        {
            _requestDelegate = requestDelegate;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            // Log request information
            _logger.LogInformation($"LoggingMiddleware: {context.Request.Method} {context.Request.Path}");
            try
            {
                await _requestDelegate.Invoke(context);
            }
            catch (Exception error)
            {
                _logger.LogError($"LoggingMiddleware: Something is wrong {error}");

                await HandleExceptionMessageAsync(context, error);
            }
        }

        private Task HandleExceptionMessageAsync(HttpContext context, Exception exception)
        { 
            int statusCode = (int)HttpStatusCode.InternalServerError;
            var result = JsonConvert.SerializeObject(new
            {
                StatusCode = statusCode,
                ErrorMessage = exception.Message
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(result);
        }
    }
}
