namespace Contonso.SampleApi.Application.Authors.Commands.CreateAuthor;

using Contonso.SampleApi.Application.Authors.Events;
using Contonso.SampleApi.Application.Common.Abstraction;
using Contonso.SampleApi.Domain.Entities;
using MediatR;

internal sealed class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, Guid>
{
    private readonly IAppDbContext dbContext;

    private readonly IPublisher mediator;

    public CreateAuthorCommandHandler(IAppDbContext dbContext, IPublisher mediator, IJobClient jobClient)
    {
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task<Guid> Handle(
        CreateAuthorCommand request,
        CancellationToken cancellationToken)
    {
        _ = request ?? throw new ArgumentNullException(nameof(request));

        var entity = new Author
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
        };

        await this.dbContext.Authors.AddAsync(entity, cancellationToken);
        await this.dbContext.SaveChangesAsync(cancellationToken);
        await this.mediator.Publish(new AuthorCreatedEvent(entity.Id), cancellationToken);

        return entity.Id;
    }
}
