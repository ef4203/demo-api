namespace Contonso.SampleApi.Application.Books.Commands.CreateBook;

using FluentValidation;

public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        this.RuleFor(o => o.Title)
            .MinimumLength(1)?
            .NotEmpty();

        this.RuleFor(o => o.AuthorId)
            .NotEmpty();

        this.RuleFor(o => o.PublishDate)
            .LessThan(DateTime.Now)
            .NotEmpty();
    }
}
