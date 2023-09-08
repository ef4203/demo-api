namespace Contonso.SampleApi.Application.Books.Commands.CreateBook;

using Contonso.SampleApi.Application.Common.Abstraction;
using Contonso.SampleApi.Domain.Entities;
using MediatR;

internal sealed class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Guid>
{
    private readonly IRepository<Book> repository;

    public CreateBookCommandHandler(IRepository<Book> repository)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
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

        await this.repository.AddAsync(entity, cancellationToken);
        await this.repository.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
