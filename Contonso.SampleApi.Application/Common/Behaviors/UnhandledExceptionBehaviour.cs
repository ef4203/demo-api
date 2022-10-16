namespace Contonso.SampleApi.Application.Common.Behaviors;

using MediatR;
using Microsoft.Extensions.Logging;

internal class UnhandledExceptionBehaviour<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger logger;

    public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _ = request ?? throw new ArgumentNullException(nameof(request));
        _ = next ?? throw new ArgumentNullException(nameof(next));

        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Unhandled exception in {0}", typeof(TRequest).Name);
            throw;
        }
    }
}
