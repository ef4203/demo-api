namespace Contonso.SampleApi.Application.Common.Behaviors;

using Contonso.SampleApi.Application.Common.LoggerMessages;
using MediatR;
using Microsoft.Extensions.Logging;

internal sealed class UnhandledExceptionBehaviour<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger logger;

    public UnhandledExceptionBehaviour(ILogger<UnhandledExceptionBehaviour<TRequest, TResponse>> logger)
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

        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            this.logger.LogUnhandledException(ex, typeof(TRequest).Name);
            throw;
        }
    }
}
