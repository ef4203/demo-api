namespace Contonso.SampleApi.Application.Authors.Events;

using MediatR;
using Microsoft.Extensions.Logging;

public record AuthorDeletedEvent(Guid Id) : INotification
{
    public Guid Id { get; set; } = Id;
}

internal sealed class AuthorDeletedEventHandler : INotificationHandler<AuthorDeletedEvent>
{
    private readonly ILogger<AuthorDeletedEvent> logger;

    public AuthorDeletedEventHandler(ILogger<AuthorDeletedEvent> logger)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public Task Handle(AuthorDeletedEvent notification, CancellationToken cancellationToken)
    {
        _ = notification ?? throw new ArgumentNullException(nameof(notification));

        this.logger.LogInformation("Author with id '{Id}' deleted.", notification.Id);

        return Task.CompletedTask;
    }
}
