namespace Contonso.SampleApi.Application.Authors.Commands.UpdateAuthor;

using FluentValidation;

internal class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
{
    public UpdateAuthorCommandValidator()
    {
        this.RuleFor(o => o.Id)
            .NotEmpty();

        this.RuleFor(o => o.FirstName)
            .MinimumLength(1)
            .NotEmpty();

        this.RuleFor(o => o.LastName)
            .MinimumLength(1)
            .NotEmpty();
    }
}
