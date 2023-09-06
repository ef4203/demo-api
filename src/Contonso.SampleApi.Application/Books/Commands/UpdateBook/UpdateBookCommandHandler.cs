namespace Contonso.SampleApi.Application.Books.Commands.UpdateBook;

using Contonso.SampleApi.Application.Common.Abstraction;
using Contonso.SampleApi.Application.Common.Exceptions;
using Contonso.SampleApi.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

internal sealed class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand>
{
    private readonly IAppDbContext dbContext;

    public UpdateBookCommandHandler(IAppDbContext dbContext)
    {
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

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
    }
}
