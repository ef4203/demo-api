namespace Contonso.SampleApi.Application.Books.Queries.GetBooks;

using Contonso.SampleApi.Application.Abstraction;
using Contonso.SampleApi.Domain;
using Mapster;
using MediatR;

public record GetBooksQuery : IRequest<IEnumerable<BookDto>>;

internal sealed class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, IEnumerable<BookDto>>
{
    private readonly IRepository<Book> repository;

    public GetBooksQueryHandler(IRepository<Book> repository)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<IEnumerable<BookDto>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        return (await this.repository.GetAllAsync(cancellationToken))
            .Adapt<IEnumerable<BookDto>>();
    }
}
