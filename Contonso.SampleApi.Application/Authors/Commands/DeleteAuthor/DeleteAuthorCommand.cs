namespace Contonso.SampleApi.Application.Authors.Commands.DeleteAuthor;

using MediatR;

public record DeleteAuthorCommand(Guid id) : IRequest<Unit>
{
    public Guid Id { get; set; } = id;
}
