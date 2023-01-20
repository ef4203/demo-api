namespace Contonso.SampleApi.Application.Common.Behaviors;

using Contonso.SampleApi.Application.Common.LoggerMessages;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

internal sealed class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger logger;

    public LoggingBehavior(ILogger<TRequest> logger)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _ = request ?? throw new ArgumentNullException(nameof(request));
        _ = next ?? throw new ArgumentNullException(nameof(next));

        this.logger.LogRequestStart(typeof(TRequest).Name, JsonConvert.SerializeObject(request));

        var response = await next();

        this.logger.LogRequestEnd(typeof(TRequest).Name, JsonConvert.SerializeObject(response));

        return response;
    }
}
