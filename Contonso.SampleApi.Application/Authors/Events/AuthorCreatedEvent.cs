namespace Contonso.SampleApi.Application.Authors.Events;

using MediatR;
using Microsoft.Extensions.Logging;

public record AuthorCreatedEvent : INotification;

public class AuthorCreatedEventHandler : INotificationHandler<AuthorCreatedEvent>
{
    private readonly ILogger logger;

    public AuthorCreatedEventHandler(ILogger<AuthorCreatedEvent> logger)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public Task Handle(AuthorCreatedEvent notification, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("AUTHOR CREATED");

        return Task.CompletedTask;
    }
}