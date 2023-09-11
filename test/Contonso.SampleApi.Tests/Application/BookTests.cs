namespace Contonso.SampleApi.Tests.Application;

using Bogus;
using Contonso.SampleApi.Application.Books.Commands.CreateBook;
using Contonso.SampleApi.Application.Books.Commands.DeleteBook;
using Contonso.SampleApi.Application.Books.Commands.UpdateBook;
using Contonso.SampleApi.Application.Books.Queries.GetBooks;
using Contonso.SampleApi.Application.Exceptions;
using Contonso.SampleApi.Tests.Application.Common;
using MediatR;
using ValidationException = Contonso.SampleApi.Application.Exceptions.ValidationException;

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

    private IMediator Mediator
    {
        get => this.Get<IMediator>();
    }

    [Test]
    public async Task CreateBookCommandRCannotBeNullAsync()
    {
        var action =
            () => this.Mediator.Send((CreateBookCommand)null!);

        await action.Should().ThrowAsync<ArgumentNullException>();
    }

    [Test]
    public async Task DeleteBookCommandCannotBeNullAsync()
    {
        var action =
            () => this.Mediator.Send((DeleteBookCommand)null!);

        await action.Should().ThrowAsync<ArgumentNullException>();
    }

    [Test]
    public async Task UpdateBookCommandCannotBeNullAsync()
    {
        var action =
            () => this.Mediator.Send((UpdateBookCommand)null!);

        await action.Should().ThrowAsync<ArgumentNullException>();
    }

    [Test]
    public async Task BookPublishDateCannotBeInTheFutureOnCreateAsync()
    {
        var command = this.createBookCommandFaker.Generate();
        command.PublishDate = DateTime.Now.AddYears(10);

        var action =
            () => this.Mediator.Send(command);

        await action.Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task BookPublishDateCannotBeNullOnCreateAsync()
    {
        var command = this.createBookCommandFaker.Generate();
        command.PublishDate = default;

        var action =
            () => this.Mediator.Send(command);

        await action.Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task BookTitleCannotBeNullOnCreateAsync()
    {
        var command = this.createBookCommandFaker.Generate();
        command.Title = null;

        var action =
            () => this.Mediator.Send(command);

        await action.Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task BookTitleCannotBeEmptyStringOnCreateAsync()
    {
        var command = this.createBookCommandFaker.Generate();
        command.Title = string.Empty;

        var action =
            () => this.Mediator.Send(command);

        await action.Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task BookAuthorIdCannotBeNullOnCreateAsync()
    {
        var command = this.createBookCommandFaker.Generate();
        command.AuthorId = Guid.Empty;

        var action =
            () => this.Mediator.Send(command);

        await action.Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task DeleteBookAsync()
    {
        var command = this.createBookCommandFaker.Generate();
        var id = await this.Mediator.Send(command);

        id.Should().NotBeEmpty();

        var action =
            () => this.Mediator.Send(new DeleteBookCommand(id));

        await action.Should().NotThrowAsync();
    }

    [Test]
    public async Task CreateBookAsync()
    {
        var command = this.createBookCommandFaker.Generate();
        var id = await this.Mediator.Send(command);

        id.Should().NotBeEmpty();
    }

    [Test]
    public async Task UpdateBookAsync()
    {
        var createCommand = this.createBookCommandFaker.Generate();

        var id = await this.Mediator.Send(createCommand);

        id.Should().NotBeEmpty();

        var updateCommand = this.updateBookCommandFaker.Generate();
        updateCommand.Id = id;

        var action =
            () => this.Mediator.Send(updateCommand);

        await action.Should().NotThrowAsync();
    }

    [Test]
    public async Task CannotUpdateNonExistingBookAsync()
    {
        var updateCommand = this.updateBookCommandFaker.Generate();

        var action =
            async () => await this.Mediator.Send(updateCommand);

        await action.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task CannotDeleteNonExistingBookAsync()
    {
        var action =
            async () => await this.Mediator.Send(new DeleteBookCommand(Guid.NewGuid()));

        await action.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task GetBooksAsync()
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
