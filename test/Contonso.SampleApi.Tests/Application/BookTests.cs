namespace Contonso.SampleApi.Tests.Application;

using Bogus;
using Contonso.SampleApi.Application.Books.Commands.CreateBook;
using Contonso.SampleApi.Application.Books.Commands.DeleteBook;
using Contonso.SampleApi.Application.Books.Commands.UpdateBook;
using Contonso.SampleApi.Application.Books.Queries.GetBooks;
using Contonso.SampleApi.Application.Common.Exceptions;
using Contonso.SampleApi.Tests.Application.Common;
using ValidationException = Contonso.SampleApi.Application.Common.Exceptions.ValidationException;

public class BookTests : BaseTest
{
    private readonly Faker<CreateBookCommand> createBookCommandFaker;

    private readonly Faker<UpdateBookCommand> updateBookCommandFaker;

    public BookTests()
    {
        this.createBookCommandFaker = new Faker<CreateBookCommand>()
            .RuleFor(o => o.Title, f => f.Commerce.ProductName())
            .RuleFor(o => o.AuthorId, f => f.Random.Guid())
            .RuleFor(o => o.PublishDate, f => f.Date.Past());
        this.updateBookCommandFaker = new Faker<UpdateBookCommand>()
            .RuleFor(o => o.Id, f => f.Random.Guid())
            .RuleFor(o => o.Title, f => f.Commerce.ProductName())
            .RuleFor(o => o.AuthorId, f => f.Random.Guid())
            .RuleFor(o => o.PublishDate, f => f.Date.Past());
    }

    [Test]
    public void CreateBookCommandRCannotBeNull()
    {
        var action =
            async () => await this.Mediator.Send((CreateBookCommand)null!);

        action.Should().ThrowAsync<ArgumentNullException>();
    }

    [Test]
    public void DeleteBookCommandCannotBeNull()
    {
        var action =
            async () => await this.Mediator.Send((DeleteBookCommand)null!);

        action.Should().ThrowAsync<ArgumentNullException>();
    }

    [Test]
    public void UpdateBookCommandCannotBeNull()
    {
        var action =
            async () => await this.Mediator.Send((UpdateBookCommand)null!);

        action.Should().ThrowAsync<ArgumentNullException>();
    }

    [Test]
    public async Task BookPublishDateCannotBeInTheFutureOnCreate()
    {
        var command = this.createBookCommandFaker.Generate();
        command.PublishDate = DateTime.Now.AddYears(10);

        var action =
            async () => await this.Mediator.Send(command);

        await action.Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task BookPublishDateCannotBeNullOnCreate()
    {
        var command = this.createBookCommandFaker.Generate();
        command.PublishDate = default;

        var action =
            async () => await this.Mediator.Send(command);

        await action.Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task BookTitleCannotBeNullOnCreate()
    {
        var command = this.createBookCommandFaker.Generate();
        command.Title = null;

        var action =
            async () => await this.Mediator.Send(command);

        await action.Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task BookTitleCannotBeEmptyStringOnCreate()
    {
        var command = this.createBookCommandFaker.Generate();
        command.Title = string.Empty;

        var action =
            async () => await this.Mediator.Send(command);

        await action.Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task BookAuthorIdCannotBeNullOnCreate()
    {
        var command = this.createBookCommandFaker.Generate();
        command.AuthorId = Guid.Empty;

        var action =
            async () => await this.Mediator.Send(command);

        await action.Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task DeleteBook()
    {
        var command = this.createBookCommandFaker.Generate();
        var id = await this.Mediator.Send(command);

        id.Should().NotBeEmpty();

        var action =
            async () => await this.Mediator.Send(new DeleteBookCommand(id));

        await action.Should().NotThrowAsync();
    }

    [Test]
    public async Task CreateBook()
    {
        var command = this.createBookCommandFaker.Generate();
        var id = await this.Mediator.Send(command);

        id.Should().NotBeEmpty();
    }

    [Test]
    public async Task UpdateBook()
    {
        var createCommand = this.createBookCommandFaker.Generate();

        var id = await this.Mediator.Send(createCommand);

        id.Should().NotBeEmpty();

        var updateCommand = this.updateBookCommandFaker.Generate();
        updateCommand.Id = id;

        var action =
            async () => await this.Mediator.Send(updateCommand);

        await action.Should().NotThrowAsync();
    }

    [Test]
    public async Task CannotUpdateNonExistingBook()
    {
        var updateCommand = this.updateBookCommandFaker.Generate();

        var action =
            async () => await this.Mediator.Send(updateCommand);

        await action.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task CannotDeleteNonExistingBook()
    {
        var action =
            async () => await this.Mediator.Send(new DeleteBookCommand(Guid.NewGuid()));

        await action.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task GetBooks()
    {
        for (var i = 0; i < 10; i++)
        {
            var command = this.createBookCommandFaker.Generate();
            await this.Mediator.Send(command);
        }

        var result =
            await this.Mediator.Send(new GetBooksQuery());

        result.Should().NotBeEmpty().And.NotBeNull();
    }
}
