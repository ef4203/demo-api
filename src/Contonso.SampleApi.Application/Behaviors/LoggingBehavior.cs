namespace Contonso.SampleApi.Application.Behaviors;

using System.Text.Json;
using Contonso.SampleApi.Application.LoggerMessages;
using MediatR;
using Microsoft.Extensions.Logging;

internal sealed class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
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

        this.logger.LogRequestStart(typeof(TRequest).Name, JsonSerializer.Serialize(request));

        var response = await next();

        this.logger.LogRequestEnd(typeof(TRequest).Name, JsonSerializer.Serialize(response));

        return response;
    }
}
