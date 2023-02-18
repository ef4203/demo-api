namespace Contonso.SampleApi.Application.Authors.Commands.DeleteAuthor;

using Contonso.SampleApi.Application.Authors.Events;
using Contonso.SampleApi.Application.Common.Abstraction;
using Contonso.SampleApi.Application.Common.Exceptions;
using Contonso.SampleApi.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

internal sealed class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand>
{
    private readonly IAppDbContext dbContext;

    private readonly IPublisher mediator;

    public DeleteAuthorCommandHandler(IAppDbContext dbContext, IPublisher mediator, IJobClient jobClient)
    {
        this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        _ = request ?? throw new ArgumentNullException(nameof(request));

        var entity = await this.dbContext.Authors.Where(x => x.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException(nameof(Book), request.Id);
        }

        this.dbContext.Authors.Remove(entity);
        await this.dbContext.SaveChangesAsync(cancellationToken);
        await this.mediator.Publish(new AuthorDeletedEvent(entity.Id), cancellationToken);
    }
}
