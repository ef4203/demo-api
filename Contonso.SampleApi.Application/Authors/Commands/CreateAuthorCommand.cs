namespace Contonso.SampleApi.Application.Authors.Commands;

using FluentValidation;
using MediatR;

public record CreateAuthorCommand : IRequest<int>
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }
}

public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorCommandValidator()
    {
        this.RuleFor(o => o.FirstName).MinimumLength(1).NotEmpty();

        this.RuleFor(o => o.LastName).MinimumLength(1).NotEmpty();
    }
}

public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, int>
{
    private readonly IApplicationDbContext dbContext;

    public CreateAuthorCommandHandler(IApplicationDbContext dbContext)
    {
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<int> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        _ = request ?? throw new ArgumentNullException(nameof(request));

        var entity = new Author { FirstName = request.FirstName, LastName = request.LastName };

        await this.dbContext.Authors.AddAsync(entity, cancellationToken);
        return await this.dbContext.SaveChangesAsync(cancellationToken);
    }
}
