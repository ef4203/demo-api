namespace Contonso.SampleApi.Application.Authors.Events;

using MediatR;
using Microsoft.Extensions.Logging;

public record AuthorCreatedEvent : INotification;

public class AuthorCreatedEventHandler : INotificationHandler<AuthorCreatedEvent>
{
    private readonly ILogger logger;

    private readonly IApplicationBackgroundJobService applicationBackgroundJobService;

    public AuthorCreatedEventHandler(ILogger<AuthorCreatedEvent> logger, IApplicationBackgroundJobService applicationBackgroundJobService)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.applicationBackgroundJobService =
            applicationBackgroundJobService ?? throw new ArgumentNullException(nameof(applicationBackgroundJobService));
    }

    public Task Handle(AuthorCreatedEvent notification, CancellationToken cancellationToken)
    {
        this.applicationBackgroundJobService.Enqueue(() => this.Event());
        return Task.CompletedTask;
    }

    public Task Event()
    {
        this.logger.LogInformation("AUTHOR CREATED");
        return Task.CompletedTask;
    }
}