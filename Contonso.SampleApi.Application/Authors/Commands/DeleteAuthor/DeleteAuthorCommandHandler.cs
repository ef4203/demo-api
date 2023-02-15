namespace Contonso.SampleApi.Application.Authors.Commands.DeleteAuthor;

using Contonso.SampleApi.Application.Authors.Events;
using Contonso.SampleApi.Application.Common.Abstraction;
using Contonso.SampleApi.Application.Common.Exceptions;
using Contonso.SampleApi.Application.Common.Extensions;
using Contonso.SampleApi.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

public sealed class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand>
{
    private readonly IAppDbContext dbContext;

    private readonly IJobClient jobClient;

    private readonly IPublisher mediator;

    public DeleteAuthorCommandHandler(IAppDbContext dbContext, IPublisher mediator, IJobClient jobClient)
    {
        this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        this.jobClient = jobClient ?? throw new ArgumentNullException(nameof(jobClient));
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
        this.mediator.PublishInBackground(new AuthorDeletedEvent(entity.Id), this.jobClient, cancellationToken);
    }
}
