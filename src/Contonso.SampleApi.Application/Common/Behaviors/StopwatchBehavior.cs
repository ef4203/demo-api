namespace Contonso.SampleApi.Application.Common.Behaviors;

using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

public class StopwatchBehavior<TRequest, TRespose> : IPipelineBehavior<TRequest, TRespose>
    where TRequest : IBaseRequest
{
    private readonly ILogger<TRequest> logger;

    public StopwatchBehavior(ILogger<TRequest> logger)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<TRespose> Handle(
        TRequest request,
        RequestHandlerDelegate<TRespose> next,
        CancellationToken cancellationToken)
    {
        _ = request ?? throw new ArgumentNullException(nameof(request));
        _ = next ?? throw new ArgumentNullException(nameof(next));

        var stopwatch = new Stopwatch();

        stopwatch.Start();
        var response = await next();
        stopwatch.Stop();

        this.logger.LogInformation(
            "Request {Request} completed after {Time}ms.",
            typeof(TRequest).Name,
            stopwatch.ElapsedMilliseconds);

        return response;
    }
}
