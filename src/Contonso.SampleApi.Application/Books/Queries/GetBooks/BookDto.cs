namespace Contonso.SampleApi.Application.Books.Queries.GetBooks;

public class BookDto
{
    public Guid Id { get; set; }

    public Guid AuthorId { get; set; }

    public string? Title { get; set; }

    public DateTime PublishDate { get; set; }
}
