namespace Contonso.SampleApi.Application.Books.Commands;

using FluentValidation;
using MediatR;

public record CreateBookCommand : IRequest<int>
{
    public string? Title { get; set; }

    public Guid AuthorId { get; set; }

    public DateTime PublishDate { get; set; }
}

public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        this.RuleFor(o => o.Title)
            .MinimumLength(1)
            .NotEmpty();

        this.RuleFor(o => o.AuthorId)
            .NotEmpty();

        this.RuleFor(o => o.PublishDate)
            .LessThan(DateTime.Now)
            .NotEmpty();
    }
}

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, int>
{
    private readonly IApplicationDbContext dbContext;

    public CreateBookCommandHandler(IApplicationDbContext dbContext)
    {
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<int> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        _ = request ?? throw new ArgumentNullException(nameof(request));

        var entity = new Book
        {
            PublishDate = request.PublishDate, AuthorId = request.AuthorId, Title = request.Title,
        };

        await this.dbContext.Books.AddAsync(entity, cancellationToken);
        return await this.dbContext.SaveChangesAsync(cancellationToken);
    }
}
