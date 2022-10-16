namespace Contonso.SampleApi.Application.Authors.Commands.DeleteAuthor;

using FluentValidation;

internal class DeleteAuthorCommandValidator : AbstractValidator<DeleteAuthorCommand>
{
    public DeleteAuthorCommandValidator()
    {
        this.RuleFor(o => o.Id)?
            .NotEmpty();
    }
}
