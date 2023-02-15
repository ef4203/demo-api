namespace Contonso.SampleApi.Application.Authors.Commands.DeleteAuthor;

using MediatR;

public record DeleteAuthorCommand(Guid Id) : IRequest
{
    public Guid Id { get; set; } = Id;
}
