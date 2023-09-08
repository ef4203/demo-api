namespace Contonso.SampleApi.Application.Authors.Commands.CreateAuthor;

using Contonso.SampleApi.Application.Authors.Events;
using Contonso.SampleApi.Application.Common.Abstraction;
using Contonso.SampleApi.Domain.Entities;
using MediatR;

internal sealed class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, Guid>
{
    private readonly IPublisher mediator;

    private readonly IRepository<Author> repository;

    public CreateAuthorCommandHandler(IRepository<Author> repository, IPublisher mediator)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task<Guid> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var entity = new Author
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
        };

        await this.repository.AddAsync(entity, cancellationToken);
        await this.repository.SaveChangesAsync(cancellationToken);
        await this.mediator.Publish(new AuthorCreatedEvent(entity.Id), cancellationToken);

        return entity.Id;
    }
}
