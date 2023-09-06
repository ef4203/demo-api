namespace Contonso.SampleApi.Application.Books.Commands.CreateBook;

using Contonso.SampleApi.Application.Common.Abstraction;
using Contonso.SampleApi.Domain.Entities;
using MediatR;

internal sealed class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Guid>
{
    private readonly IAppDbContext dbContext;

    public CreateBookCommandHandler(IAppDbContext dbContext)
    {
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<Guid> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var entity = new Book
        {
            PublishDate = request.PublishDate,
            AuthorId = request.AuthorId,
            Title = request.Title,
        };

        await this.dbContext.Books.AddAsync(entity, cancellationToken);
        await this.dbContext.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
