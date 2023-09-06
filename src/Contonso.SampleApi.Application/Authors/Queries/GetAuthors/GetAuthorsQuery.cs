namespace Contonso.SampleApi.Application.Authors.Queries.GetAuthors;

using AutoMapper;
using AutoMapper.QueryableExtensions;
using Contonso.SampleApi.Application.Common.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

public record GetAuthorsQuery : IRequest<IEnumerable<AuthorDto>>;

internal sealed class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, IEnumerable<AuthorDto>>
{
    private readonly IAppDbContext dbContext;

    private readonly IMapper mapper;

    public GetAuthorsQueryHandler(IAppDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IEnumerable<AuthorDto>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
    {
        return await this.dbContext.Authors
            .ProjectTo<AuthorDto>(this.mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
