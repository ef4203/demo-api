namespace Contonso.SampleApi.Application.Authors.Commands.DeleteAuthor;

using MediatR;
using Microsoft.EntityFrameworkCore;

internal sealed class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, Unit>
{
    private readonly IApplicationDbContext dbContext;

    public DeleteAuthorCommandHandler(IApplicationDbContext dbContext)
    {
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<Unit> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
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

        return Unit.Value;
    }
}