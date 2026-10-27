using Escola.API.Errors;
using Escola.Application.Exceptions;
using System.Text.Json;

namespace Escola.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                int statusCode;
                switch (ex)
                {
                    case NotFoundException:
                        statusCode = StatusCodes.Status404NotFound;
                        break;
                    case BadRequestException:
                        statusCode = StatusCodes.Status400BadRequest;
                        break;
                    case UnauthorizedAccessException:
                        statusCode = StatusCodes.Status401Unauthorized;
                        break;
                    default:
                        statusCode = StatusCodes.Status500InternalServerError;
                        break;
                }


                httpContext.Response.StatusCode = statusCode;
                httpContext.Response.ContentType = "application/json";
                var response = _env.IsDevelopment()
                    ? new ApiException(statusCode.ToString(), ex.Message, ex.StackTrace?.ToString() ?? "No stack trace available")
                    : new ApiException(statusCode.ToString(), ex.Message, "Internal server error");
                
                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response, options);
                
                await httpContext.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
