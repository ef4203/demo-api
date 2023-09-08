namespace Contonso.SampleApi.Application.Books.Commands.UpdateBook;

using Contonso.SampleApi.Application.Common.Abstraction;
using Contonso.SampleApi.Application.Common.Exceptions;
using Contonso.SampleApi.Domain.Entities;
using MediatR;

internal sealed class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand>
{
    private readonly IRepository<Book> repository;

    public UpdateBookCommandHandler(IRepository<Book> repository)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var entity = await this.repository.GetAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException(nameof(Book), request.Id);
        }

        entity.Title = request.Title;
        entity.AuthorId = request.AuthorId;
        entity.PublishDate = request.PublishDate;

        await this.repository.SaveChangesAsync(cancellationToken);
    }
}
