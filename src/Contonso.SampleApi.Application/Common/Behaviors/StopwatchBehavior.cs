namespace Contonso.SampleApi.Application.Common.Behaviors;

using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

public class StopwatchBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseRequest
{
    private readonly ILogger logger;

    public StopwatchBehavior(ILogger<StopwatchBehavior<TRequest, TResponse>> logger)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(next);

        var stopwatch = new Stopwatch();

        stopwatch.Start();
        var response = await next();
        stopwatch.Stop();

        this.logger.LogInformation(
            "Request {Request} completed after {Time}ms",
            typeof(TRequest).Name,
            stopwatch.ElapsedMilliseconds);

        return response;
    }
}
