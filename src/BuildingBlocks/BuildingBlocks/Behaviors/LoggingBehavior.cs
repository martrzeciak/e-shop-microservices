using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger) 
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        logger.LogInformation($"[START] HANDLE REQUEST=[{typeof(TRequest).Name}] - " +
            $"RESPONSE=[{typeof(TResponse).Name}] - RequestData=[{request}]");

        Stopwatch timer = new();
        timer.Start();

        var response = await next();

        timer.Stop();
        var timeTaken = timer.Elapsed;

        if (timeTaken.Seconds > 3)
        {
            logger.LogWarning($"[PERFORMANCE] REQUEST [{typeof(TRequest).Name}] took" +
                $" {timeTaken.Seconds} seconds");
        }

        logger.LogInformation($"[END] HANDLED {typeof(TRequest).Name} WITH {typeof(TResponse).Name}");

        return response;
    }
}
