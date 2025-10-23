using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Shared.Behaviors;

public class LoggingBehavior<TRequest , TResponse>
             (ILogger<LoggingBehavior<TRequest, TResponse>> logger)
             : IPipelineBehavior<TRequest, TResponse>
             where TRequest : notnull, IRequest<TResponse>
             where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("[START] Handle request = {Request} - Response = {Response} - Request = {TRequest}"
            ,typeof(TRequest).Name,typeof(TResponse).Name,request);
        var timer = new Stopwatch();
        timer.Start();
        var response = await next(cancellationToken);
        timer.Stop();
        var elapsed = timer.Elapsed;
        if (elapsed.Seconds > 3)
            logger.LogWarning($"Taken {elapsed.Seconds} seconds for {typeof(TRequest).Name}");
        
        return response;
    }
}