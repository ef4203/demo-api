namespace Contonso.SampleApi.Application.Books.Commands.CreateBook;

using MediatR;

public record CreateBookCommand : IRequest<Guid>
{
    public string? Title { get; set; }

    public Guid AuthorId { get; set; }

    public DateTime PublishDate { get; set; }
}
