namespace Contonso.SampleApi.Application.Books.Commands.DeleteBook;

using MediatR;

public record DeleteBookCommand(Guid id) : IRequest<Unit>
{
    public Guid Id { get; set; } = id;
}
