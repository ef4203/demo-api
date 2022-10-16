namespace Contonso.SampleApi.Application.Books.Commands.DeleteBook;

using MediatR;

public record DeleteBookCommand(Guid Id) : IRequest<Unit>
{
    public Guid Id { get; set; } = Id;
}
