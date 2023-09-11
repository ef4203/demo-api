namespace Contonso.SampleApi.Application.Books.Commands.DeleteBook;

using Contonso.SampleApi.Application.Abstraction;
using Contonso.SampleApi.Application.Exceptions;
using Contonso.SampleApi.Domain;
using MediatR;

internal sealed class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand>
{
    private readonly IRepository<Book> repository;

    public DeleteBookCommandHandler(IRepository<Book> repository)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (await this.repository.GetAsync(request.Id, cancellationToken) == null)
        {
            throw new NotFoundException(nameof(Book), request.Id);
        }

        await this.repository.DeleteAsync(request.Id, cancellationToken);
        await this.repository.SaveChangesAsync(cancellationToken);
    }
}
