namespace Contonso.SampleApi.Application.Authors.Queries.GetAuthors;

using Contonso.SampleApi.Application.Common.Abstraction;
using Contonso.SampleApi.Domain.Entities;
using Mapster;
using MediatR;

public record GetAuthorsQuery : IRequest<IEnumerable<AuthorDto>>;

internal sealed class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, IEnumerable<AuthorDto>>
{
    private readonly IRepository<Author> repository;

    public GetAuthorsQueryHandler(IRepository<Author> repository)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<IEnumerable<AuthorDto>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
    {
        return (await this.repository.GetAllAsync(cancellationToken))
            .Adapt<IEnumerable<AuthorDto>>();
    }
}
