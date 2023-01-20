namespace Contonso.SampleApi.Application.Authors.Commands.DeleteAuthor;

using FluentValidation;

internal sealed class DeleteAuthorCommandValidator : AbstractValidator<DeleteAuthorCommand>
{
    public DeleteAuthorCommandValidator()
    {
        this.RuleFor(o => o.Id)
            .NotEmpty();
    }
}
