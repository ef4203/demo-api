namespace Contonso.SampleApi.Application.Books.Commands;

using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

public record DeleteBookCommand(Guid id) : IRequest<Unit>
{
    public Guid Id { get; set; } = id;
}

public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
{
    public DeleteBookCommandValidator()
    {
        this.RuleFor(o => o.Id).NotEmpty();
    }
}

public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Unit>
{
    private readonly IApplicationDbContext dbContext;

    public DeleteBookCommandHandler(IApplicationDbContext dbContext)
    {
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        _ = request ?? throw new ArgumentNullException(nameof(request));

        var entity = await this.dbContext.Books.Where(x => x.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException(nameof(Book), request.Id);
        }

        this.dbContext.Books.Remove(entity);
        await this.dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}