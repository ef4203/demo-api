namespace Contonso.SampleApi.Application.Books.Queries.GetBooks;

using Contonso.SampleApi.Application.Common.Mapping;
using Contonso.SampleApi.Domain.Entities;

public class BookDto : IMapFrom<Book>
{
    public Guid Id { get; set; }

    public Guid AuthorId { get; set; }

    public string? Title { get; set; }

    public DateTime PublishDate { get; set; }
}
