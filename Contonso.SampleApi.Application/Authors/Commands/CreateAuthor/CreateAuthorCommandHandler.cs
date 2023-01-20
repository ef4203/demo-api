namespace Contonso.SampleApi.Application.Authors.Commands.CreateAuthor;

using Contonso.SampleApi.Application.Authors.Events;
using MediatR;

internal sealed class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, Guid>
{
    private readonly IApplicationDbContext dbContext;

    private readonly IMediator mediator;

    public CreateAuthorCommandHandler(IApplicationDbContext dbContext, IMediator mediator)
    {
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task<Guid> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        _ = request ?? throw new ArgumentNullException(nameof(request));

        var entity = new Author
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
        };

        await this.dbContext.Authors.AddAsync(entity, cancellationToken);
        await this.dbContext.SaveChangesAsync(cancellationToken);
        await this.mediator.Publish(new AuthorCreatedEvent(), cancellationToken);

        return entity.Id;
    }
}
