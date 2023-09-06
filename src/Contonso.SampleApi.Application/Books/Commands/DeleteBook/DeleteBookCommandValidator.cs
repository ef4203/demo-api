namespace Contonso.SampleApi.Application.Books.Commands.DeleteBook;

using FluentValidation;

public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
{
    public DeleteBookCommandValidator()
    {
        this.RuleFor(o => o.Id)
            ?.NotEmpty();
    }
}
