using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HospitalSystems.Application.Common.Behaviors;

public class PerformanceBehaviour<TRequest,TResponse>(ILogger<TRequest>logger):IPipelineBehavior<TRequest,TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var timer = Stopwatch.StartNew();
        var response = await next(cancellationToken);
        timer.Stop();
        if (timer.ElapsedMilliseconds > 500)
        {
            logger.LogWarning($"Slow Request: {typeof(TRequest).Name} ({timer.ElapsedMilliseconds} ms) {@request}");
        }
        return response;
    }
}
