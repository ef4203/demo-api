namespace Contonso.SampleApi.Application.Authors.Commands.UpdateAuthor;

using Contonso.SampleApi.Application.Abstraction;
using Contonso.SampleApi.Application.Exceptions;
using Contonso.SampleApi.Domain;
using MediatR;

internal sealed class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand>
{
    private readonly IRepository<Author> repository;

    public UpdateAuthorCommandHandler(IRepository<Author> repository)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var entity = await this.repository.GetAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException(nameof(Book), request.Id);
        }

        entity.FirstName = request.FirstName;
        entity.LastName = request.LastName;

        await this.repository.SaveChangesAsync(cancellationToken);
    }
}
