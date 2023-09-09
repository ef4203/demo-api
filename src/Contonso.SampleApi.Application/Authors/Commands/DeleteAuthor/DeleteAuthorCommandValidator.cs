namespace Contonso.SampleApi.Application.Authors.Commands.DeleteAuthor;

using FluentValidation;

public sealed class DeleteAuthorCommandValidator : AbstractValidator<DeleteAuthorCommand>
{
    public DeleteAuthorCommandValidator()
    {
        this.RuleFor(o => o.Id)
            ?.NotEmpty();
    }
}
