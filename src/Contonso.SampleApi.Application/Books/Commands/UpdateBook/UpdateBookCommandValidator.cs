namespace Contonso.SampleApi.Application.Books.Commands.UpdateBook;

using FluentValidation;

public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookCommandValidator()
    {
        this.RuleFor(o => o.Id)
            ?.NotEmpty();

        this.RuleFor(o => o.Title)
            ?.MinimumLength(1)
            ?.NotEmpty();

        this.RuleFor(o => o.AuthorId)
            ?.NotEmpty();

        this.RuleFor(o => o.PublishDate)
            ?.LessThan(DateTime.Now)
            ?.NotEmpty();
    }
}
