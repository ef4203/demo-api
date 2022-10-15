namespace Contonso.SampleApi.Application.Books.Commands;

using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

public record UpdateBookCommand : IRequest<Unit>
{
    public Guid Id { get; set; }

    public string? Title { get; set; }

    public Guid AuthorId { get; set; }

    public DateTime PublishDate { get; set; }
}

public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookCommandValidator()
    {
        this.RuleFor(o => o.Id).NotEmpty();

        this.RuleFor(o => o.Title).MinimumLength(1).NotEmpty();

        this.RuleFor(o => o.AuthorId).NotEmpty();

        this.RuleFor(o => o.PublishDate).LessThan(DateTime.Now).NotEmpty();
    }
}

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Unit>
{
    private readonly IApplicationDbContext dbContext;

    public UpdateBookCommandHandler(IApplicationDbContext dbContext)
    {
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<Unit> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        _ = request ?? throw new ArgumentNullException(nameof(request));

        var entity = await this.dbContext.Books.Where(o => o.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException(nameof(Book), request.Id);
        }

        entity.Title = request.Title;
        entity.AuthorId = request.AuthorId;
        entity.PublishDate = request.PublishDate;

        await this.dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}