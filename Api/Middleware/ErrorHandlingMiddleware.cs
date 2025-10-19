using Exceptions;
using System.Net;

namespace Api.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                _logger.LogInformation("Handling request: {RequestPath}", context.Request.Path);
                await _next(context);
                _logger.LogInformation("Finished handling request.");
            }
            catch (CustomException ex)
            {
                _logger.LogInformation("Custom exception caught: {exception}", ex);
                await HandleCustomExceptionAsync(context, ex);
            }
            catch (System.Exception ex)
            {
                _logger.LogInformation("System exception caught: {exception}", ex);
                await HandleSystemExceptionAsync(context, ex);
            }
        }

        private async Task HandleCustomExceptionAsync(HttpContext context, CustomException ex)
        {
            _logger.LogInformation("Handling custom exception : {exception}", ex);
            context.Response.ContentType = "application/json";

            switch (ex)
            {
                case BussinessRuleValidationExeption bussinessRuleValidationExeption:
                    context.Response.StatusCode = StatusCodes.Status409Conflict;
                    await context.Response.WriteAsync(
                        ErrorDetails.Create(
                            (HttpStatusCode)StatusCodes.Status409Conflict, bussinessRuleValidationExeption.Message).ToString()
                            );
                    break;

                case EntityNotFoundException entityNotFountException:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    await context.Response.WriteAsync(
                        ErrorDetails.Create(
                            (HttpStatusCode)StatusCodes.Status404NotFound, entityNotFountException.Message).ToString()
                        );
                    break;

                default:
                    await context.Response.WriteAsync(
                        ErrorDetails.Create(
                            (HttpStatusCode)StatusCodes.Status500InternalServerError, "An unexpected Error has occured.").ToString());
                    break;
            }

            _logger.LogInformation("Custom exception handled: {exception}", ex);
        }

        private async Task HandleSystemExceptionAsync(HttpContext context, object ex)
        {
            _logger.LogInformation("Handling system exception: {exception}", ex);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var errorDetails = ErrorDetails.Create((HttpStatusCode)context.Response.StatusCode, "An unexpected error has occured").ToString();

            await context.Response.WriteAsync(errorDetails);
            _logger.LogInformation("System exception handled: {exception}", ex);
        }
    }
}