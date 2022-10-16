namespace Contonso.SampleApi.Application.Authors.Queries.GetAuthors;

using Contonso.SampleApi.Application.Common.Mapping;

public class AuthorDto : IMapFrom<Author>
{
    public Guid Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }
}
