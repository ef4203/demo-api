namespace Contonso.SampleApi.Application.Books.Commands.UpdateBook;

using MediatR;
using Microsoft.EntityFrameworkCore;

internal sealed class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Unit>
{
    private readonly IApplicationDbContext dbContext;

    public UpdateBookCommandHandler(IApplicationDbContext dbContext)
    {
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<Unit> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        _ = request ?? throw new ArgumentNullException(nameof(request));

        var entity = await this.dbContext.Books
            .Where(o => o.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException(nameof(Book), request.Id);
        }

        entity.Title = request.Title;
        entity.AuthorId = request.AuthorId;
        entity.PublishDate = request.PublishDate;

        await this.dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
