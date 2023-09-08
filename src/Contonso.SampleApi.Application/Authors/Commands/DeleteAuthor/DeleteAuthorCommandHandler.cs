namespace Contonso.SampleApi.Application.Authors.Commands.DeleteAuthor;

using Contonso.SampleApi.Application.Authors.Events;
using Contonso.SampleApi.Application.Common.Abstraction;
using Contonso.SampleApi.Application.Common.Exceptions;
using Contonso.SampleApi.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

internal sealed class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand>
{
    private readonly IRepository<Author> repository;

    private readonly IPublisher mediator;

    public DeleteAuthorCommandHandler(IRepository<Author> repository, IPublisher mediator)
    {
        this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (await this.repository.GetAsync(request.Id, cancellationToken) == null)
        {
            throw new NotFoundException(nameof(Book), request.Id);
        }

        await this.repository.DeleteAsync(request.Id, cancellationToken);
        await this.repository.SaveChangesAsync(cancellationToken);
        await this.mediator.Publish(new AuthorDeletedEvent(request.Id), cancellationToken);
    }
}
