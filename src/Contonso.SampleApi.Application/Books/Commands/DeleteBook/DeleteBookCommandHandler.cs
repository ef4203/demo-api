namespace Contonso.SampleApi.Application.Books.Commands.DeleteBook;

using Contonso.SampleApi.Application.Common.Abstraction;
using Contonso.SampleApi.Application.Common.Exceptions;
using Contonso.SampleApi.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

internal sealed class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand>
{
    private readonly IAppDbContext dbContext;

    public DeleteBookCommandHandler(IAppDbContext dbContext)
    {
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        _ = request ?? throw new ArgumentNullException(nameof(request));

        var entity = await this.dbContext.Books
            .Where(x => x.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException(nameof(Book), request.Id);
        }

        this.dbContext.Books.Remove(entity);
        await this.dbContext.SaveChangesAsync(cancellationToken);
    }
}
