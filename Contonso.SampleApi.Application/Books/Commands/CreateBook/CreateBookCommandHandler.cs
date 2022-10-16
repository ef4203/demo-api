namespace Contonso.SampleApi.Application.Books.Commands.CreateBook;

using MediatR;

internal class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Guid>
{
    private readonly IApplicationDbContext dbContext;

    public CreateBookCommandHandler(IApplicationDbContext dbContext)
    {
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<Guid> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        _ = request ?? throw new ArgumentNullException(nameof(request));

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
