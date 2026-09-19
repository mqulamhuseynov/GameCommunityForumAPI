using Microsoft.AspNetCore.Diagnostics;
using GamingForum.Application.Exceptions.ApplicationExceptions;
using Microsoft.AspNetCore.Mvc;
using GamingForum.Application.Exceptions.BaseException;

namespace GamingForum.API.Handlers
{
    public sealed class GlobalExceptionHandler(
        IProblemDetailsService problemDetailsService,
        ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var status = exception switch
            {
                ValidationFailedException => StatusCodes.Status400BadRequest,
                NotFoundException => StatusCodes.Status404NotFound,
                ForbiddenException => StatusCodes.Status403Forbidden,
                UnauthorizedException => StatusCodes.Status401Unauthorized,
                ConflictException => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };

            if(status == StatusCodes.Status500InternalServerError)
                logger.LogError(exception, "An unhandled exception occurred while processing the request.");

            ProblemDetails problem = exception is ValidationFailedException validation 
                ? new ValidationProblemDetails(validation.Errors) 
                : new ProblemDetails();

            problem.Status = status;
            problem.Detail = exception is AppException and not ValidationFailedException 
                ? exception.Message 
                : null;

            httpContext.Response.StatusCode = status;

            return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
            {
                HttpContext = httpContext,
                ProblemDetails = problem,
                Exception = exception
            });
        }
    }
}
