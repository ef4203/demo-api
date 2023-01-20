namespace Contonso.SampleApi.Application.Authors.Commands.CreateAuthor;

using FluentValidation;

internal sealed class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorCommandValidator()
    {
        this.RuleFor(o => o.FirstName)
            .MinimumLength(1)?
            .NotEmpty();

        this.RuleFor(o => o.LastName)
            .MinimumLength(1)?
            .NotEmpty();
    }
}
