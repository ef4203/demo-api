namespace Contonso.SampleApi.Application.Authors.Commands.DeleteAuthor;

using MediatR;

public record DeleteAuthorCommand(Guid Id) : IRequest<Unit>
{
    public Guid Id { get; set; } = Id;
}
