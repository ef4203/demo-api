namespace Contonso.SampleApi.Application.Authors.Commands.CreateAuthor;

using MediatR;

public record CreateAuthorCommand : IRequest<Guid>
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }
}
