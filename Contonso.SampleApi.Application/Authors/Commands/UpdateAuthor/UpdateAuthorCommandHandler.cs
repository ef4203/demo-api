namespace Contonso.SampleApi.Application.Authors.Commands;

using MediatR;
using Microsoft.EntityFrameworkCore;

internal class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, Unit>
{
    private readonly IApplicationDbContext dbContext;

    public UpdateAuthorCommandHandler(IApplicationDbContext dbContext)
    {
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<Unit> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        _ = request ?? throw new ArgumentNullException(nameof(request));

        var entity = await this.dbContext.Authors.Where(o => o.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException(nameof(Book), request.Id);
        }

        entity.FirstName = request.FirstName;
        entity.LastName = request.LastName;

        await this.dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
