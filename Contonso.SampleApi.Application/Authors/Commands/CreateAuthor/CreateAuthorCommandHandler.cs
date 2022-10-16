namespace Contonso.SampleApi.Application.Authors.Commands.CreateAuthor;

using MediatR;

internal class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, Guid>
{
    private readonly IApplicationDbContext dbContext;

    public CreateAuthorCommandHandler(IApplicationDbContext dbContext)
    {
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<Guid> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        _ = request ?? throw new ArgumentNullException(nameof(request));

        var entity = new Author
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
        };

        await this.dbContext.Authors.AddAsync(entity, cancellationToken);
        await this.dbContext.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
