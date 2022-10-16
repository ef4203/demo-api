namespace Contonso.SampleApi.Application.Books.Commands.UpdateBook;

using MediatR;

public record UpdateBookCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public Guid AuthorId { get; set; }
    public DateTime PublishDate { get; set; }
}
