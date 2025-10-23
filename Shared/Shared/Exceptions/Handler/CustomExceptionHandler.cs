using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Shared.Exceptions.Handler;

public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(exception, exception.Message);

        (string Title, int StatusCode) details = exception switch
        {
            ValidationException =>
            (
                exception.Message,
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest
            ),
            
            NotFoundException =>
            (
                exception.Message,
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest
            ),
            
            BadRequestException =>
            (
                exception.Message,
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest
            ),
            
            _ =>
            (
                exception.Message,
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest
            )
        };
        var problemDetails = new ProblemDetails
        {
            Title = details.Title,
            Status = details.StatusCode
        };
        problemDetails.Extensions.Add("traceID", httpContext.TraceIdentifier);

        if (exception is ValidationException validationException)
        {
            problemDetails.Extensions.Add("ValidationError", validationException.Message);
        }
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);
        return true;
    }
}