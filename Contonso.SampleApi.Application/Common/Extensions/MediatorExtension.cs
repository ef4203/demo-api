namespace Contonso.SampleApi.Application.Common.Extensions;

using Contonso.SampleApi.Application.Common.Abstraction;
using MediatR;

public static class MediatorExtension
{
    public static void PublishInBackground(
        this IPublisher mediator,
        INotification request,
        IJobClient jobClient,
        CancellationToken cancellationToken)
    {
        _ = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _ = request ?? throw new ArgumentNullException(nameof(request));
        _ = jobClient ?? throw new ArgumentNullException(nameof(jobClient));

        jobClient.Enqueue(() => mediator.Publish(request, cancellationToken));
    }
}
