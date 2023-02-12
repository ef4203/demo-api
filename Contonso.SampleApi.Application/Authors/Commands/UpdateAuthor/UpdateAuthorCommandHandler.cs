namespace Contonso.SampleApi.Application.Authors.Commands.UpdateAuthor;

using Contonso.SampleApi.Application.Common.Abstraction;
using Contonso.SampleApi.Application.Common.Exceptions;
using Contonso.SampleApi.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

internal sealed class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, Unit>
{
    private readonly IAppDbContext dbContext;

    public UpdateAuthorCommandHandler(IAppDbContext dbContext)
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
