namespace Contonso.SampleApi.Application.Authors.Events;

using Contonso.SampleApi.Application.Common.Abstraction;
using MediatR;
using Microsoft.Extensions.Logging;

public record AuthorCreatedEvent(Guid Id) : INotification
{
    public Guid Id { get; set; } = Id;
}

public class AuthorCreatedEventHandler : INotificationHandler<AuthorCreatedEvent>
{
    private readonly ILogger logger;

    public AuthorCreatedEventHandler(
        ILogger<AuthorCreatedEvent> logger,
        IJobClient jobClient)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Handle(AuthorCreatedEvent notification, CancellationToken cancellationToken)
    {
        _ = notification ?? throw new ArgumentNullException(nameof(notification));

        await Task.Delay(5000, cancellationToken);
        this.logger.LogInformation("Author with id '{Id}' created.", notification.Id);
    }
}
