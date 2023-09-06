namespace Contonso.SampleApi.Application.Authors.Events;

using MediatR;
using Microsoft.Extensions.Logging;

public record AuthorCreatedEvent(Guid Id) : INotification
{
    public Guid Id { get; set; } = Id;
}

internal sealed class AuthorCreatedEventHandler : INotificationHandler<AuthorCreatedEvent>
{
    private readonly ILogger logger;

    public AuthorCreatedEventHandler(ILogger<AuthorCreatedEvent> logger)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public Task Handle(AuthorCreatedEvent notification, CancellationToken cancellationToken)
    {
        _ = notification ?? throw new ArgumentNullException(nameof(notification));

        this.logger.LogInformation("Author with id '{Id}' created.", notification.Id);

        return Task.CompletedTask;
    }
}
