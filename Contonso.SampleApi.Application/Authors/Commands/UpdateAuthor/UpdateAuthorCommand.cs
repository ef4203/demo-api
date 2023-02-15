namespace Contonso.SampleApi.Application.Authors.Commands.UpdateAuthor;

using MediatR;

public record UpdateAuthorCommand : IRequest
{
    public Guid Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }
}
