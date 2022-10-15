namespace Contonso.SampleApi.Application.Books.Queries.GetBooks;

using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

public record GetBooksQuery : IRequest<IEnumerable<BookDto>>;

public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, IEnumerable<BookDto>>
{
    private readonly IApplicationDbContext dbContext;

    private readonly IMapper mapper;

    public GetBooksQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IEnumerable<BookDto>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        return await this.dbContext.Books
            .ProjectTo<BookDto>(this.mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
