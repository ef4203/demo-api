namespace Contonso.SampleApi.Application.Common.Behaviors;

using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

internal class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger logger;

    public LoggingBehavior(ILogger<TRequest> logger)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _ = request ?? throw new ArgumentNullException(nameof(request));
        _ = next ?? throw new ArgumentNullException(nameof(next));

        this.logger.LogDebug("Start of {0}, Request: {1}", typeof(TRequest).Name,
            JsonConvert.SerializeObject(request));
        var response = await next();
        this.logger.LogDebug("End of {0}, Response: {1}", typeof(TRequest).Name,
            JsonConvert.SerializeObject(response));

        return response;
    }
}
